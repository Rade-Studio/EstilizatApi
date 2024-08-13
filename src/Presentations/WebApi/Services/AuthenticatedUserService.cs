using System;
using Microsoft.AspNetCore.Http;
using Services.Interfaces;
using System.Security.Claims;

namespace WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserEmail = httpContextAccessor.HttpContext?.User?.FindFirstValue(
                ClaimTypes.Email);

            UserId = new Guid(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                              string.Empty);
        }

        public string UserEmail { get; }
        public Guid UserId { get; }
    }
}