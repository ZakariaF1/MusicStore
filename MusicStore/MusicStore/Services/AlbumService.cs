using MusicStore.Domain;
using MusicStore.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ITrackRepository _trackRepository;


        public AlbumService(IAlbumRepository albumRepository, ITrackRepository trackRepository)
        {
            _albumRepository = albumRepository;
            _trackRepository = trackRepository;
        }

        public async Task DeleteAlbumCascadeAsync(Album album)
        {
            IQueryable<Track> tracksQuery = _trackRepository.TrackBaseQuery();

            tracksQuery = tracksQuery.Where(track => track.AlbumId == album.AlbumId);

            IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery);

            foreach (Track track in tracks)
            {
                track.AlbumId = null;
            }
            _albumRepository.DeleteAlbum(album);
        }
    }
}
