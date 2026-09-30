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
    public class TrackController : Controller
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ITrackService _trackService;
        public TrackController(ITrackRepository trackRepository,
                                IMediaTypeRepository mediaTypeRepository,
                                IAlbumRepository albumRepository,
                                IPlaylistRepository playlistRepository,
                                IGenreRepository genreRepository,
                                ITrackService trackService)
        {
            _trackRepository = trackRepository;
            _mediaTypeRepository = mediaTypeRepository;
            _albumRepository = albumRepository;
            _playlistRepository = playlistRepository;
            _genreRepository = genreRepository;
            _trackService = trackService;
        }


        [HttpGet("api/v1/tracks")]
        public async Task<IActionResult> ListTracks()
        {

            IQueryable<Track> tracksQuery = _trackRepository.TrackBaseQuery();

            tracksQuery = tracksQuery.OrderBy(track => track.TrackId);

            IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery, true);

            IEnumerable<TrackResponse> response = tracks.Select(p => p.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/tracks")]
        public async Task<IActionResult> CreateTrack([FromBody] TrackCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool albumExists = await _albumRepository.AlbumExistsAsync(request.AlbumId);
            bool mediaTypeExists = await _mediaTypeRepository.MediaTypeExistsAsync(request.MediaTypeId);
            bool genreExists = await _genreRepository.GenreExistsAsync(request.GenreId);

            if (!albumExists && request.AlbumId.HasValue == true)
            {
                return BadRequest("Album wasn't found");
            }
            if (!genreExists && request.GenreId.HasValue == true)
            {
                return BadRequest("Genre wasn't found");
            }
            if (!mediaTypeExists)
            {
                return BadRequest("Media Type wasn't found");
            }
            else
            {
                Track track = request.ToEntity();

                await _trackRepository.CreateTrackAsync(track);

                await _trackRepository.SaveAsync();

                TrackLiteResponse response = track.ToLiteResponseDto();

                return CreatedAtRoute("GetTrackById", new { trackId = track.TrackId }, response);
            }
        }

        [HttpGet("api/v1/tracks/{trackId}", Name = "GetTrackById")]
        public async Task<IActionResult> GetTrack([FromRoute] int trackId)
        {
            Track track = await _trackRepository.GetTrackAsync(trackId);
            if (track == null)
            {
                return NotFound("Track wasn't found");
            }
            else
            {
                TrackResponse response = track.ToResponseDto();

                return Ok(response);
            }
        }

        [HttpDelete("api/v1/tracks/{trackId}")]
        public async Task<IActionResult> DeleteTrack([FromRoute] int trackId)
        {
            Track track = await _trackRepository.GetTrackAsync(trackId);
            if (track == null)
            {
                return NotFound("Track wasn't found");
            }
            else
            {
                //_trackRepository.DeleteTrack(track);
                await _trackService.DeleteTrackCascadeAsync(track);
                await _trackRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/tracks/{trackId}")]
        public async Task<IActionResult> UpdateTrack([FromRoute] int trackId, [FromBody] TrackUpdateRequest request)
        {
            Track track = await _trackRepository.GetTrackAsync(trackId);
            if (track == null)
            {
                return NotFound("Track wasn't found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool albumExists = await _albumRepository.AlbumExistsAsync(request.AlbumId);
            bool mediaTypeExists = await _mediaTypeRepository.MediaTypeExistsAsync(request.MediaTypeId);
            bool genreExists = await _genreRepository.GenreExistsAsync(request.GenreId);

            if (!albumExists && request.AlbumId.HasValue == true)
            {
                return BadRequest("Album wasn't found");
            }
            if (!genreExists && request.GenreId.HasValue == true)
            {
                return BadRequest("Genre wasn't found");
            }
            if (!mediaTypeExists)
            {
                return BadRequest("Media Type wasn't found");
            }
            else
            {
                track.UpdateTrack(request);
                await _trackRepository.SaveAsync();

                TrackLiteResponse response = track.ToLiteResponseDto();

                return Ok(response);
            }
        }

        [HttpGet("api/v1/tracks/{trackId}/playlistTracks")]
        public async Task<IActionResult> ListPlaylistTracks([FromRoute] int trackId)
        {
            bool trackeExists = await _trackRepository.TrackExistsAsync(trackId);

            if (!trackeExists)
            {
                return NotFound("Track wasn't found");
            }
            else
            {
                IQueryable<PlaylistTrack> playlistTracksQuery = _playlistRepository.PlaylistTrackBaseQuery();

                playlistTracksQuery = playlistTracksQuery
                    .Where(playlistTrack => playlistTrack.TrackId == trackId)
                    .OrderBy(playlistTrack => playlistTrack.TrackId);

                IEnumerable<PlaylistTrack> playlistTracks = await _playlistRepository.ListPlaylistTracksAsync(playlistTracksQuery, true);

                IEnumerable<PlaylistTrackResponse> response = playlistTracks.Select(p => p.ToResponseDto());

                return Ok(response);
            }
        }
    }
}
