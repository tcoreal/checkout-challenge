using System.Security.Claims;

namespace Checkout.Infrastructure.Fake
{
    public class FakeUserService : IUserService
    {
        public ulong GetCurrentUserId(ClaimsPrincipal principal) => 555;
    }
}