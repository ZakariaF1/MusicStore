using MusicStore.Domain;
using MusicStore.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumService _albumService;
        private readonly IAlbumRepository _albumRepository;

        public ArtistService(IArtistRepository artistRepository, IAlbumService albumService, IAlbumRepository albumRepository)
        {
            _artistRepository = artistRepository;
            _albumService = albumService;
            _albumRepository = albumRepository;
        }

        public async Task DeleteArtistCascadeAsync(Artist artist)
        {
            IQueryable<Album> albumsQuery = _albumRepository.AlbumBaseQuery();

            albumsQuery = albumsQuery.Where(album => album.ArtistId == artist.ArtistId);

            IEnumerable<Album> albums = await _albumRepository.ListAlbumsAsync(albumsQuery);

            foreach (Album album in albums)
            {
                await _albumService.DeleteAlbumCascadeAsync(album);
            }
            _artistRepository.DeleteArtist(artist);
        }
    }
}
