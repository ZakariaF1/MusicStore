using MusicStore.Domain;
using MusicStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class MediaTypeService : IMediaTypeService
    {
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly ITrackService _trackService;
        private readonly IInvoiceRepository _invoiceRepository;

        public MediaTypeService(IMediaTypeRepository mediaTypeRepository, ITrackRepository trackRepository, ITrackService trackService, IInvoiceRepository invoiceRepository)
        {
            _mediaTypeRepository = mediaTypeRepository;
            _trackRepository = trackRepository;
            _trackService = trackService;
            _invoiceRepository = invoiceRepository;
        }

        public async Task DeleteMediaTypeCascadeAsync(MediaType mediaType)
        {
            IQueryable<Track> tracksQuery = _trackRepository.TrackBaseQuery();

            tracksQuery = tracksQuery.Where(p => p.MediaTypeId == mediaType.MediaTypeId);

            IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery);

            foreach (Track track in tracks)
            {
                await _trackService.DeleteTrackCascadeAsync(track);
            }
            _mediaTypeRepository.DeleteMediaType(mediaType);
        }
    }
}
