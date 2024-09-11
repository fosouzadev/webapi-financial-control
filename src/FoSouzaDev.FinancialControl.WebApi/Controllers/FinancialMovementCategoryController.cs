using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FoSouzaDev.FinancialControl.WebApi.Controllers;

[Route("api/v1/financial-movement-category")]
public sealed class FinancialMovementCategoryController(IFinancialMovementCategoryAppService appService)
    : ApplicationControllerBase<FinancialMovementCategory, FinancialMovementCategoryDto, UpdateFinancialMovementCategoryDto, AddFinancialMovementCategoryDto>(appService)
{
}