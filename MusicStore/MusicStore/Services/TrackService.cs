using MusicStore.Domain;
using MusicStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class TrackService : ITrackService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceService _invoiceService;
        private readonly ITrackRepository _trackRepository;
        private readonly IPlaylistRepository _playlistRepository;

        public TrackService(IInvoiceRepository invoiceRepository, IInvoiceService invoiceService, ITrackRepository trackRepository, IPlaylistRepository playlistRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceService = invoiceService;
            _trackRepository = trackRepository;
            _playlistRepository = playlistRepository;

        }

        public async Task DeleteTrackCascadeAsync(Track track)
        {
            IQueryable<InvoiceItem> invoiceItemsQuery = _invoiceRepository.InvoiceItemBaseQuery();

            invoiceItemsQuery = invoiceItemsQuery.Where(p => p.TrackId == track.TrackId);

            IEnumerable<InvoiceItem> invoiceItems = await _invoiceRepository.ListInvoiceItemsAsync(invoiceItemsQuery);

            foreach (InvoiceItem invoiceItem in invoiceItems)
            {
                Invoice invoice = await _invoiceRepository.GetInvoiceAsync(invoiceItem.InvoiceId);

                await _invoiceService.DeleteInvoiceItemCascadeAsync(invoiceItem);
            }
            IQueryable<PlaylistTrack> playlistTracksQuery = _playlistRepository.PlaylistTrackBaseQuery();

            playlistTracksQuery = playlistTracksQuery.Where(playlistTrack => playlistTrack.TrackId == track.TrackId);

            IEnumerable<PlaylistTrack> playlistTracks = await _playlistRepository.ListPlaylistTracksAsync(playlistTracksQuery);

            foreach (PlaylistTrack playlistTrack in playlistTracks)
            {
                _playlistRepository.DeletePlaylistTrack(playlistTrack);
            }
            _trackRepository.DeleteTrack(track);
        }
    }
}
