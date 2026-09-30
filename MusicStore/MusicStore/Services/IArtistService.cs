using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public interface IArtistService
    {
        Task DeleteArtistCascadeAsync(Artist artist);
    }
}
