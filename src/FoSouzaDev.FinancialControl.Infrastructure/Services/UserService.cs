namespace FoSouzaDev.FinancialControl.Infrastructure.Services;

internal class UserService(IHttpContextAccessor httpContextAccessor) : IUserAppService
{
    private const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    public Guid GetUserId() =>
        new(httpContextAccessor.HttpContext.User.Claims.Single(a => a.Type == ObjectIdentifier).Value);
}