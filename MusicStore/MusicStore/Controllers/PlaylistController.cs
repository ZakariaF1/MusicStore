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
    public class PlaylistController : Controller
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IPlaylistService _playlistService;
        public PlaylistController(IPlaylistRepository playlistRepository, ITrackRepository trackRepository, IPlaylistService playlistService)
        {
            _playlistRepository = playlistRepository;
            _trackRepository = trackRepository;
            _playlistService = playlistService;
        }


        [HttpGet("api/v1/playlists")]
        public async Task<IActionResult> ListPlaylists()
        {
            IQueryable<Playlist> playlistsQuery = _playlistRepository.PlaylistBaseQuery();

            playlistsQuery = playlistsQuery.OrderBy(playlist => playlist.PlaylistId);

            IEnumerable<Playlist> playlists = await _playlistRepository.ListPlaylistsAsync(playlistsQuery, true);

            IEnumerable<PlaylistResponse> response = playlists.Select(playlist => playlist.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/playlists")]
        public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Playlist playlist = request.ToEntity();

            await _playlistRepository.CreatePlaylistAsync(playlist);

            await _playlistRepository.SaveAsync();

            PlaylistResponse response = playlist.ToResponseDto();

            return CreatedAtRoute("GetPlaylistById", new { playlistId = playlist.PlaylistId }, response);
        }

        [HttpGet("api/v1/playlists/{playlistId}", Name = "GetPlaylistById")]
        public async Task<IActionResult> GetPlaylist([FromRoute] int playlistId)
        {
            Playlist playlist = await _playlistRepository.GetPlaylistAsync(playlistId);
            if (playlist == null)
            {
                return NotFound("Playlist wasn't found");
            }
            else
            {
                PlaylistResponse response = playlist.ToResponseDto();
                return Ok(response);
            }
        }

        [HttpDelete("api/v1/playlists/{playlistId}")]
        public async Task<IActionResult> DeletePlaylist([FromRoute] int playlistId)
        {
            Playlist playlist = await _playlistRepository.GetPlaylistAsync(playlistId);
            if (playlist == null)
            {
                return NotFound("Playlist wasn't found");
            }
            else
            {
                //_playlistRepository.DeletePlaylist(playlist);
                await _playlistService.DeletePlaylistCascadeAsync(playlist);
                await _playlistRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/playlists/{playlistId}")]
        public async Task<IActionResult> UpdatePlaylist([FromRoute] int playlistId, [FromBody] PlaylistUpdateRequest request)
        {
            Playlist playlist = await _playlistRepository.GetPlaylistAsync(playlistId);
            if (playlist == null)
            {
                return NotFound("Playlist wasn't found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            playlist.UpdatePlaylist(request);
            await _playlistRepository.SaveAsync();

            return Ok(playlist);
        }

        [HttpGet("api/v1/playlists/{playlistId}/playlistTracks")]
        public async Task<IActionResult> ListPlaylistTracks([FromRoute] int playlistId)
        {
            bool playlistExists = await _playlistRepository.PlaylistExistsAsync(playlistId);

            if (!playlistExists)
            {
                return NotFound("Playlist wasn't found");
            }
            else
            {
                IQueryable<PlaylistTrack> playlistTracksQuery = _playlistRepository.PlaylistTrackBaseQuery();

                playlistTracksQuery = playlistTracksQuery.Where(playlistTrack => playlistTrack.PlaylistId == playlistId);

                IEnumerable<PlaylistTrack> playlistTracks = await _playlistRepository.ListPlaylistTracksAsync(playlistTracksQuery, true);

                IEnumerable<PlaylistTrackResponse> response = playlistTracks.Select(playlistTrack => playlistTrack.ToResponseDto());

                return Ok(response);
            }
        }

        [HttpPost("api/v1/playlists/{playlistId}/playlistTracks")]
        public async Task<IActionResult> CreatePlaylistTrack([FromRoute] int playlistId, [FromBody] PlaylistTrackCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool playlistExists = await _playlistRepository.PlaylistExistsAsync(playlistId);

            if (!playlistExists)
            {
                return NotFound("Playlist wasn't found");
            }

            bool trackExists = await _playlistRepository.TrackExistsAsync(request.TrackId);

            if (!trackExists)
            {
                return NotFound("Track wasn't found");
            }

            bool playlistTrackExists = await _playlistRepository.PlaylistTrackExistsAsync(playlistId, request.TrackId);

            if (playlistTrackExists)
            {
                return BadRequest("Track is already in the playlist");
            }

            PlaylistTrack playlistTrack = request.ToEntity();
            playlistTrack.PlaylistId = playlistId;

            await _playlistRepository.CreatePlaylistTrackAsync(playlistTrack);

            await _playlistRepository.SaveAsync();

            PlaylistTrackLiteResponse response = playlistTrack.ToLiteResponseDto();

            return Ok(response);
        }

        [HttpDelete("api/v1/playlists/{playlistId}/playlistTracks/{playlistTrackId}")]
        public async Task<IActionResult> DeletePlaylistTrack([FromRoute]int playlistId, [FromRoute] int playlistTrackId)
        {
            bool playlistExists = await _playlistRepository.PlaylistExistsAsync(playlistId);

            if (!playlistExists)
            {
                return NotFound("Playlist wasn't found");
            }

            PlaylistTrack playlistTrack = await _playlistRepository.GetPlaylistTrackAsync(playlistId, playlistTrackId);
            if (playlistTrack == null)
            {
                return NotFound("PlaylistTrack wasn't found");
            }
            else
            {
                _playlistRepository.DeletePlaylistTrack(playlistTrack);
                await _playlistRepository.SaveAsync();
                return NoContent();
            }
        }
    }
}

