using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FoSouzaDev.FinancialControl.Application.Services;

internal class UserAppService(IHttpContextAccessor httpContextAccessor) : IUserAppService
{
    private const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    public Guid GetUserId() =>
        new(httpContextAccessor.HttpContext.User.Claims.Single(a => a.Type == ObjectIdentifier).Value);
}