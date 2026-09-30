using MusicStore.Domain;
using MusicStore.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ITrackRepository _trackRepository;

        public GenreService(IGenreRepository genreRepository, ITrackRepository trackRepository)
        {
            _genreRepository = genreRepository;
            _trackRepository = trackRepository;
        }

        public async Task DeleteGenreCascadeAsync(Genre genre)
        {
            IQueryable<Track> tracksQuery = _trackRepository.TrackBaseQuery();

            tracksQuery = tracksQuery.Where(track => track.GenreId == genre.GenreId);

            IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery);

            foreach (Track track in tracks)
            {
                track.GenreId = null;
            }
            _genreRepository.DeleteGenre(genre);
        }
    }
}
