using blazorauth.services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace blazorauth.fakeservices
{
    public class FakeService : IService
    {
        public async Task<ClaimsIdentity> LoginAsync(ClaimsIdentity identity)
        {
            await Task.Delay(0);
            return identity;
        }

        public async Task<ClaimsIdentity> LoginImpersonatorAsync(ClaimsIdentity identity, string username, string password)
        {
            await Task.Delay(0);
            return identity;
        }

        public async Task<ClaimsIdentity> LogoutAsync(ClaimsIdentity identity)
        {
            await Task.Delay(0);
            return identity;
        }

        public async Task<ClaimsIdentity> LogoutImpersonatorAsync(ClaimsIdentity identity)
        {
            await Task.Delay(0);
            return identity;
        }
    }
}
