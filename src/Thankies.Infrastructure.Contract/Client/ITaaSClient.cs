using System.Threading;
using System.Threading.Tasks;
using Thankies.Infrastructure.Contract.Model;

namespace Thankies.Infrastructure.Contract.Client
{
    public interface ITaaSClient
    {
        Task<GratitudeResponse> GetGratitude(string? name, string? filter, string language = "eng", CancellationToken cancellationToken = default);
    }
}