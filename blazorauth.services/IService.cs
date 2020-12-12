using System.Security.Claims;
using System.Threading.Tasks;

namespace blazorauth.services
{
    public interface IService
    {
        Task<ClaimsIdentity> LoginAsync(ClaimsIdentity identity);

        Task<ClaimsIdentity> LogoutAsync(ClaimsIdentity identity);

        Task<ClaimsIdentity> LoginImpersonatorAsync(ClaimsIdentity identity, string username, string password);

        Task<ClaimsIdentity> LogoutImpersonatorAsync(ClaimsIdentity identity);
    }
}
