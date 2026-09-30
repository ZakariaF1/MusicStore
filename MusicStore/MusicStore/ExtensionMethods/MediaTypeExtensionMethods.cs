using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.ExtensionMethods
{
    public static class MediaTypeExtensionMethods
    {
        public static MediaTypeResponse ToResponseDto(this MediaType mediaType)
        {
            return new MediaTypeResponse
            {
                MediaTypeId = mediaType.MediaTypeId,
                Name = mediaType.Name
            };
        }

        public static MediaType ToEntity(this MediaTypeCreateRequest request)
        {
            return new MediaType
            {
                Name = request.Name
            };
        }

        public static MediaType UpdateMediaType(this MediaType mediaType, MediaTypeUpdateRequest request)
        {

            mediaType.Name = request.Name;

            return mediaType;
        }
    }
}
