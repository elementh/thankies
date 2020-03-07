using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Thankies.Infrastructure.Contract.Service
{
    public interface IGratitudeService
    {
        Task<string?> Get(string? name = null, string? filter = null, string category = "basic", string language = "eng", CancellationToken cancellationToken = default);
        Task<List<string>> GetForEveryFilter(string? name = null, string language = "eng", CancellationToken cancellationToken = default);
    }
}