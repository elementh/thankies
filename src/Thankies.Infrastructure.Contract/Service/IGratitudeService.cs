using System.Threading;
using System.Threading.Tasks;

namespace Thankies.Infrastructure.Contract.Service
{
    public interface IGratitudeService
    {
        Task<string?> Get(string? name, string? filter, string language = "eng", CancellationToken cancellationToken = default);
    }
}