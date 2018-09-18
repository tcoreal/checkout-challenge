using System.Security.Claims;

namespace Checkout.Infrastructure
{
    public interface IUserService
    {
        ulong GetCurrentUserId(ClaimsPrincipal principal);
    }
}