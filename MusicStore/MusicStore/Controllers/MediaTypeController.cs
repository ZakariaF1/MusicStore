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
    public class MediaTypeController : Controller
    {
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IMediaTypeService _mediaTypeService;
        public MediaTypeController(IMediaTypeRepository mediaTypeRepository, ITrackRepository trackRepository, IMediaTypeService mediaTypeService)
        {
            _mediaTypeRepository = mediaTypeRepository;
            _trackRepository = trackRepository;
            _mediaTypeService = mediaTypeService;
        }


        [HttpGet("api/v1/mediaTypes")]
        public async Task<IActionResult> ListMediaTypes()
        {
            IQueryable<MediaType> mediaTypesQuery = _mediaTypeRepository.MediaTypeBaseQuery();

            mediaTypesQuery = mediaTypesQuery.OrderBy(mediaType => mediaType.MediaTypeId);

            IEnumerable<MediaType> mediaTypes = await _mediaTypeRepository.ListMediaTypesAsync(mediaTypesQuery, true);

            IEnumerable<MediaTypeResponse> response = mediaTypes.Select(mediaType => mediaType.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/mediaTypes")]
        public async Task<IActionResult> CreateMediaType([FromBody] MediaTypeCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MediaType mediaType = request.ToEntity();

            await _mediaTypeRepository.CreateMediaTypeAsync(mediaType);

            await _mediaTypeRepository.SaveAsync();

            MediaTypeResponse response = mediaType.ToResponseDto();

            return CreatedAtRoute("GetMediaTypeById", new { mediaTypeId = mediaType.MediaTypeId }, response);
        }

        [HttpGet("api/v1/mediaTypes/{mediaTypeId}", Name = "GetMediaTypeById")]
        public async Task<IActionResult> GetMediaType([FromRoute] int mediaTypeId)
        {
            MediaType mediaType = await _mediaTypeRepository.GetMediaTypeAsync(mediaTypeId);
            if (mediaType == null)
            {
                return NotFound("Media Type wasn't found");
            }
            else
            {
                MediaTypeResponse response = mediaType.ToResponseDto();
                return Ok(response);
            }
        }

        [HttpDelete("api/v1/mediaTypes/{mediaTypeId}")]
        public async Task<IActionResult> DeleteMediaType([FromRoute] int mediaTypeId)
        {
            MediaType mediaType = await _mediaTypeRepository.GetMediaTypeAsync(mediaTypeId);
            if (mediaType == null)
            {
                return NotFound("Media Type wasn't found");
            }
            else
            {
                //_mediaTypeRepository.DeleteMediaType(mediaType);
                await _mediaTypeService.DeleteMediaTypeCascadeAsync(mediaType);
                await _mediaTypeRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/mediaTypes/{mediaTypeId}")]
        public async Task<IActionResult> UpdateMediaType([FromRoute] int mediaTypeId, [FromBody] MediaTypeUpdateRequest request)
        {
            MediaType mediaType = await _mediaTypeRepository.GetMediaTypeAsync(mediaTypeId);
            if (mediaType == null)
            {
                return NotFound("Media Type wasn't found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            mediaType.UpdateMediaType(request);
            await _mediaTypeRepository.SaveAsync();

            MediaTypeResponse response = mediaType.ToResponseDto();

            return Ok(response);
        }

        [HttpGet("api/v1/mediaTypes/{mediaTypeId}/tracks")]
        public async Task<IActionResult> ListTracks([FromRoute] int mediaTypeId)
        {
            bool mediaTypeExists = await _mediaTypeRepository.MediaTypeExistsAsync(mediaTypeId);

            if (!mediaTypeExists)
            {
                return NotFound("Media Type wasn't found");
            }
            else
            {
                IQueryable<Track> tracksQuery = _trackRepository.TrackBaseQuery();

                tracksQuery = tracksQuery
                    .Where(track => track.MediaTypeId == mediaTypeId)
                    .OrderBy(track => track.TrackId);

                IEnumerable<Track> tracks = await _trackRepository.ListTracksAsync(tracksQuery, true);

                IEnumerable<TrackResponse> response = tracks.Select(track => track.ToResponseDto());

                return Ok(response);
            }
        }
    }
}

