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
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumRepository albumRepository, IArtistRepository artistRepository, ITrackRepository trackRepository, IAlbumService albumService)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _trackRepository = trackRepository;
            _albumService = albumService;
        }


        [HttpGet("api/v1/albums")]
        public async Task<IActionResult> ListAlbums()
        {
            IQueryable<Album> albumsQuery = _albumRepository.AlbumBaseQuery();

            albumsQuery = albumsQuery.OrderBy(album => album.AlbumId);

            IEnumerable<Album> albums = await _albumRepository.ListAlbumsAsync(albumsQuery, true);

            IEnumerable<AlbumResponse> response = albums.Select(p => p.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/albums")]
        public async Task<IActionResult> CreateAlbum([FromBody] AlbumCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool artistExists = await _artistRepository.ArtistExistsAsync(request.ArtistId);

            if (!artistExists)
            {
                return BadRequest("Artist wasn't found");
            }
            else
            {
                Album album = request.ToEntity();

                await _albumRepository.CreateAlbumAsync(album);

                await _albumRepository.SaveAsync();

                AlbumLiteResponse response = album.ToLiteResponseDto();

                return CreatedAtRoute("GetAlbumById", new { albumId = album.AlbumId }, response);
            }
        }

        [HttpGet("api/v1/albums/{albumId}", Name = "GetAlbumById")]
        public async Task<IActionResult> GetAlbum([FromRoute] int albumId)
        {
            Album album = await _albumRepository.GetAlbumAsync(albumId);
            if (album == null)
            {
                return NotFound("Album wasn't found");
            }
            else
            {

                AlbumResponse response = album.ToResponseDto();

                return Ok(response);
            }
        }

        [HttpDelete("api/v1/albums/{albumId}")]
        public async Task<IActionResult> DeleteAlbum([FromRoute] int albumId)
        {
            Album album = await _albumRepository.GetAlbumAsync(albumId);
            if (album == null)
            {
                return NotFound("Album wasn't found");
            }
            else
            {
                //_albumRepository.DeleteAlbum(album);
                await _albumService.DeleteAlbumCascadeAsync(album);
                await _albumRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/albums/{albumId}")]
        public async Task<IActionResult> UpdateAlbum([FromRoute] int albumId, [FromBody] AlbumUpdateRequest request)
        {
            Album album = await _albumRepository.GetAlbumAsync(albumId);
            if (album == null)
            {
                return NotFound("Album wasn't found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool artistExists = await _artistRepository.ArtistExistsAsync(request.ArtistId);

            if (!artistExists)
            {
                return BadRequest("Artist wasn't found");
            }

            album.UpdateAlbum(request);
            await _albumRepository.SaveAsync();

            AlbumLiteResponse response = album.ToLiteResponseDto();

            return Ok(response);
        }

        [HttpGet("api/v1/albums/{albumId}/tracks")]
        public async Task<IActionResult> ListTracks([FromRoute] int albumId)
        {
            bool albumExists = await _albumRepository.AlbumExistsAsync(albumId);

            if (!albumExists)
            {
                return NotFound("Album wasn't found");
            }
            else
            {
                IQueryable<Track> tracksQuery = _trackRepository.TrackBaseQuery();

                tracksQuery = tracksQuery
                    .Where(track => track.AlbumId == albumId)
                    .OrderBy(track => track.TrackId);

                IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery, true);

                IEnumerable<TrackResponse> response = tracks.Select(track => track.ToResponseDto());

                return Ok(response);
            }
        }
    }
}
