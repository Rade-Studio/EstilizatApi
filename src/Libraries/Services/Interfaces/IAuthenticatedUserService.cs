using System;

namespace Services.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserEmail { get; }
        Guid UserId { get; }
    }
}