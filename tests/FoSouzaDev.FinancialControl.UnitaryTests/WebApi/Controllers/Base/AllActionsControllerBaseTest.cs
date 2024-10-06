using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces.Base;
using FoSouzaDev.FinancialControl.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Controllers.Base;

public abstract class AllActionsControllerBaseTest<TController, TAppService, TAddDto, TGetDto, TUpdateDto>
    : PartialActionsControllerBaseTest<TController, TAppService, TAddDto, TGetDto, TUpdateDto>
    where TController : AllActionsControllerBase<TAppService, TAddDto, TGetDto, TUpdateDto>
    where TAppService : class, IAllActionsAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    [Fact]
    public async Task RemoveAsync_Success_ReturnHttpResponseNoContent()
    {
        // Arrange
        Guid id = base.Fixture.Create<Guid>();

        // Act
        IResult response = await base.Controller.RemoveAsync(id);

        // Assert
        response.Should().BeOfType<NoContent>();

        base.AppServiceMock.Verify(a => a.RemoveAsync(id), Times.Once);
    }
}