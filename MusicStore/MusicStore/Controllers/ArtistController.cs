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
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistService _artistService;
        public ArtistController(IArtistRepository artistRepository, IAlbumRepository albumRepository, IArtistService artistService)
        {
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
            _artistService = artistService;
        }


        [HttpGet("api/v1/artists")]
        public async Task<IActionResult> ListArtists()
        {
            IQueryable<Artist> artistsQuery = _artistRepository.ArtistBaseQuery();

            artistsQuery = artistsQuery.OrderBy(artist => artist.ArtistId);

            IEnumerable<Artist> artists = await _artistRepository.ListArtistsAsync(artistsQuery);

            IEnumerable<ArtistResponse> response = artists.Select(artist => artist.ToResponseDto());

            return Ok(response);
        }
        [HttpPost("api/v1/artists")]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Artist artist = request.ToEntity();

            await _artistRepository.CreateArtistAsync(artist);

            await _artistRepository.SaveAsync();

            ArtistResponse response = artist.ToResponseDto();

            return CreatedAtRoute("GetArtistById", new { artistId = artist.ArtistId }, response);
        }

        [HttpGet("api/v1/artists/{artistId}", Name = "GetArtistById")]
        public async Task<IActionResult> GetArtist([FromRoute] int artistId)
        {
            Artist artist = await _artistRepository.GetArtistAsync(artistId);
            if (artist == null)
            {
                return NotFound("Artist wasn't found");
            }
            else
            {
                ArtistResponse response = artist.ToResponseDto();
                return Ok(response);
            }
        }

        [HttpDelete("api/v1/artists/{artistId}")]
        public async Task<IActionResult> DeleteArtist([FromRoute] int artistId)
        {
            Artist artist = await _artistRepository.GetArtistAsync(artistId);
            if (artist == null)
            {
                return NotFound("Artist wasn't found");
            }
            else
            {
                //_artistRepository.DeleteArtist(artist);
                await _artistService.DeleteArtistCascadeAsync(artist);
                await _artistRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/artists/{artistId}")]
        public async Task<IActionResult> UpdateArtist([FromRoute] int artistId, [FromBody] ArtistUpdateRequest request)
        {
            Artist artist = await _artistRepository.GetArtistAsync(artistId);
            if (artist == null)
            {
                return NotFound("Artist wasn't found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            artist.UpdateArtist(request);
            await _artistRepository.SaveAsync();

            ArtistResponse response = artist.ToResponseDto();

            return Ok(response);
        }

        [HttpGet("api/v1/artists/{artistId}/albums")]
        public async Task<IActionResult> ListAlbumsAsync([FromRoute] int artistId)
        {
            bool artistExists = await _artistRepository.ArtistExistsAsync(artistId);

            if (!artistExists)
            {
                return NotFound("Artist wasn't found");
            }
            else
            {
                IQueryable<Album> albumsQuery = _albumRepository.AlbumBaseQuery();

                albumsQuery = albumsQuery
                    .Where(album => album.ArtistId == artistId)
                    .OrderBy(album => album.AlbumId);

                IEnumerable<Album> albums = await _albumRepository.ListAlbumsAsync(albumsQuery);

                IEnumerable<AlbumResponse> response = albums.Select(p => p.ToResponseDto());

                return Ok(response);
            }
        }
    }
}

