using blazorauth.services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace blazorauth.webappss.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IService service;

        public CustomAuthStateProvider(IService service)
        {
            this.service = service;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            WindowsIdentity wi = WindowsIdentity.GetCurrent();

            if(wi.IsAuthenticated)
            {
                ClaimsIdentity ci = await service.LoginAsync(wi);
            }

            return new AuthenticationState(new ClaimsPrincipal(wi));
        }

        public async Task LoginAsync(string username, string password)
        {
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            if (!wi.IsAuthenticated)
            {
                NotifyAuthenticationStateChanged(GenerateEmptyAuthenticationState());
                return;
            }


            ClaimsIdentity ci = await service.LoginImpersonatorAsync(wi, username, password);
            if(ci.IsAuthenticated)
            {
                NotifyAuthenticationStateChanged(GenerateImpersonatorAuthenticationState(ci));
                return;
            }
        }

        public async Task LogoutImpersonatorAsync()
        {
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            await service.LogoutImpersonatorAsync(wi);
            NotifyAuthenticationStateChanged(GenerateWindowsAuthenticationState(wi));
        }

        private Task<AuthenticationState> GenerateImpersonatorAuthenticationState(ClaimsIdentity ci)
        {
            ci.AddClaim(new Claim(ci.RoleClaimType, "impersonator"));
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(ci)));
        }

        private Task<AuthenticationState> GenerateWindowsAuthenticationState(ClaimsIdentity ci)
        {
            ci.TryRemoveClaim(new Claim(ci.RoleClaimType, "impersonator"));
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(ci)));
        }

        private Task<AuthenticationState> GenerateEmptyAuthenticationState()
        {
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
        }
    }
}
