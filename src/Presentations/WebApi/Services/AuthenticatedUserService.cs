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

            UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"));
        }

        public string UserEmail { get; }
        public int UserId { get; }
    }
}