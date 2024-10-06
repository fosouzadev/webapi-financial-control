using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces.Base;
using FoSouzaDev.FinancialControl.WebApi.Controllers.Base;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Controllers.Base;

public abstract class PartialActionsControllerBaseTest<TController, TAppService, TAddDto, TGetDto, TUpdateDto> : BaseTest
    where TController : PartialActionsControllerBase<TAppService, TAddDto, TGetDto, TUpdateDto>
    where TAppService : class, IPartialActionsAppService<TAddDto, TGetDto, TUpdateDto>
    where TAddDto : class
    where TGetDto : class
    where TUpdateDto : class
{
    protected Mock<IUrlHelper> UrlHelperMock { get; init; }
    protected Mock<TAppService> AppServiceMock { get; init; }
    protected TController Controller { get; init; }

    protected PartialActionsControllerBaseTest()
    {
        UrlHelperMock = new();
        AppServiceMock = new();

        Controller = (TController)Activator.CreateInstance(typeof(TController), AppServiceMock.Object);
        Controller.Url = UrlHelperMock.Object;
    }

    [Fact]
    public async Task AddAsync_Success_ReturnHttpResponseCreatedWithExpectedData()
    {
        // Arrange
        TAddDto request = base.Fixture.Create<TAddDto>();
        Guid expectedId = Guid.NewGuid();
        string expectedUrl = base.Fixture.Create<string>();

        AppServiceMock.Setup(a => a.AddAsync(request)).ReturnsAsync(expectedId);
        UrlHelperMock.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns(expectedUrl);

        // Act
        IResult response = await Controller.AddAsync(request);

        // Assert
        Created<ResponseData<Guid>> created = response.Should().BeOfType<Created<ResponseData<Guid>>>().Subject;
        created.Location.Should().Be(expectedUrl);
        created.Value.Should().Match<ResponseData<Guid>>(a =>
            a.Data == expectedId &&
            a.ErrorMessage == null);
    }

    [Fact]
    public async Task GetByIdAsync_Success_ReturnHttpResponseOkWithExpectedData()
    {
        // Arrange
        Guid id = base.Fixture.Create<Guid>();
        TGetDto expectedData = base.Fixture.Create<TGetDto>();

        AppServiceMock.Setup(a => a.GetByIdAsync(id)).ReturnsAsync(expectedData);

        // Act
        IResult response = await Controller.GetByIdAsync(id);

        // Assert
        Ok<ResponseData<TGetDto>> ok = response.Should().BeOfType<Ok<ResponseData<TGetDto>>>().Subject;
        ok.Value.Should().Match<ResponseData<TGetDto>>(a =>
            a.Data == expectedData &&
            a.ErrorMessage == null);
    }

    [Fact]
    public async Task UpdateAsync_Success_ReturnHttpResponseNoContent()
    {
        // Arrange
        Guid id = base.Fixture.Create<Guid>();
        JsonPatchDocument<TUpdateDto> jsonPathDocument = base.Fixture.Build<JsonPatchDocument<TUpdateDto>>()
            .Without(a => a.ContractResolver)
            .Create();

        // Act
        IResult response = await Controller.UpdateAsync(id, jsonPathDocument);

        // Assert
        response.Should().BeOfType<NoContent>();

        AppServiceMock.Verify(a => a.UpdateAsync(id, jsonPathDocument), Times.Once);
    }
}