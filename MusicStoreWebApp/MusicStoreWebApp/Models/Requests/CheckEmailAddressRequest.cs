using MusicStore.Api.Requests.UpdateRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStoreWebApp.Models.Requests
{
    public class CheckEmailAddressRequest
    {
        public int EntityId { get; set; }
        public string Email { get; set; }
    }
}
