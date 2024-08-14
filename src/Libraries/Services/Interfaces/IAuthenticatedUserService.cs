using System;

namespace Services.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserEmail { get; }
        int UserId { get; }
    }
}