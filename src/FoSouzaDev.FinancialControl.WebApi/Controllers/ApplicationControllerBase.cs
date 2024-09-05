using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[ApiController]
[Authorize(Policy = "MicrosoftIdentityPolicy", Roles = "full,query")]
public abstract class ApplicationControllerBase : ControllerBase
{
    private const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    protected Guid UserId => new(base.User.Claims.Single(a => a.Type == ObjectIdentifier).Value);
}