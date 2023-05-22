using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Thankies.Infrastructure.Contract.Model;

namespace Thankies.Infrastructure.Contract.Client
{
    public interface ITaaSClient
    {
        Task<GratitudeResponse> GetGratitude(string? name = null, string? filter = null, string category = "basic", string language = "eng", CancellationToken cancellationToken = default);

        Task<GratitudeResponse?> GetGratitudeAllFilters(string? name = null, string language = "eng",
            CancellationToken cancellationToken = default);
    }
}