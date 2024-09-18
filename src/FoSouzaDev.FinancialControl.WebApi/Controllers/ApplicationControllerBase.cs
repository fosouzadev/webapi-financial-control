using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[ApiController]
[Authorize(Policy = "MicrosoftIdentityPolicy", Roles = "full,query")]
[Produces("application/json")]
[Consumes("application/json")]
public abstract class ApplicationControllerBase : ControllerBase
{
}