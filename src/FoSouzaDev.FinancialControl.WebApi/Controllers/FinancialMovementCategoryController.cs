using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

public sealed class FinancialMovementCategoryController : ApplicationControllerBase
{
    [HttpGet]
    public async Task<IResult> GetTestAsync()
    {
        return TypedResults.Ok(base.UserId);
    }
}