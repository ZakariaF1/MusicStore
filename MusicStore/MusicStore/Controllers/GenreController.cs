using Microsoft.AspNetCore.Mvc;
using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;
using MusicStore.Repository;
using MusicStore.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Services;

namespace MusicStore.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IGenreService _genreService;
        public GenreController(IGenreRepository genreRepository, ITrackRepository trackRepository, IGenreService genreService)
        {
            _genreRepository = genreRepository;
            _trackRepository = trackRepository;
            _genreService = genreService;
        }


        [HttpGet("api/v1/genres")]
        public async Task<IActionResult> ListGenres()
        {
            IQueryable<Genre> genresQuery = _genreRepository.GenreBaseQuery();

            genresQuery = genresQuery.OrderBy(genre => genre.GenreId);

            IEnumerable<Genre> genres = await _genreRepository.ListGenresAsync(genresQuery, true);

            IEnumerable<GenreResponse> response = genres.Select(artist => artist.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/genres")]
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Genre genre = request.ToEntity();

            await _genreRepository.CreateGenreAsync(genre);

            await _genreRepository.SaveAsync();

            GenreResponse response = genre.ToResponseDto();

            return CreatedAtRoute("GetGenreById", new { genreId = genre.GenreId }, response);
        }

        [HttpGet("api/v1/genres/{genreId}", Name = "GetGenreById")]
        public async Task<IActionResult> GetGenre([FromRoute] int genreId)
        {
            Genre genre = await _genreRepository.GetGenreAsync(genreId);
            if (genre == null)
            {
                return NotFound("Genre wasn't found");
            }
            else
            {
                GenreResponse response = genre.ToResponseDto();
                return Ok(response);
            }
        }

        [HttpDelete("api/v1/genres/{genreId}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int genreId)
        {
            Genre genre = await _genreRepository.GetGenreAsync(genreId);
            if (genre == null)
            {
                return NotFound("Genre wasn't found");
            }
            else
            {
                //_genreRepository.DeleteGenre(genre);
                await _genreService.DeleteGenreCascadeAsync(genre);
                await _genreRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/genres/{genreId}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int genreId, [FromBody] GenreUpdateRequest request)
        {
            Genre genre = await _genreRepository.GetGenreAsync(genreId);
            if (genre == null)
            {
                return NotFound("Genre wasn't found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            genre.UpdateGenre(request);
            await _genreRepository.SaveAsync();

            GenreResponse response = genre.ToResponseDto();

            return Ok(response);
        }

        [HttpGet("api/v1/genres/{genreId}/tracks")]
        public async Task<IActionResult> ListTracks([FromRoute] int genreId)
        {
            bool genreExists = await _genreRepository.GenreExistsAsync(genreId);

            if (!genreExists)
            {
                return NotFound("Genre wasn't found");
            }
            else
            {
                IQueryable<Track> tracksQuery = _trackRepository.TrackBaseQuery();

                tracksQuery = tracksQuery
                    .Where(track => track.GenreId == genreId)
                    .OrderBy(track => track.TrackId);

                IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery, true);

                IEnumerable<TrackResponse> response = tracks.Select(track => track.ToResponseDto());

                return Ok(response);
            }
        }
    }
}

