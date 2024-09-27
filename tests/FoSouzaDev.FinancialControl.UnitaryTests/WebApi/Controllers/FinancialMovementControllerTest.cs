using AutoFixture;
using FluentAssertions;
using FoSouzaDev.FinancialControl.Application.DataTransferObjects;
using FoSouzaDev.FinancialControl.Application.Services.Interfaces;
using FoSouzaDev.FinancialControl.WebApi.Controllers;
using FoSouzaDev.FinancialControl.WebApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;

namespace FoSouzaDev.FinancialControl.UnitaryTests.WebApi.Controllers;

public class FinancialMovementControllerTest : BaseTest
{
    private readonly Mock<IFinancialMovementAppService> _appServiceMock;
    private readonly Mock<IUrlHelper> _urlHelper;

    private readonly FinancialMovementController _controller;

    public FinancialMovementControllerTest()
    {
        _appServiceMock = new Mock<IFinancialMovementAppService>();
        _urlHelper = new Mock<IUrlHelper>();

        _controller = new(_appServiceMock.Object)
        {
            Url = _urlHelper.Object
        };
    }

    [Fact]
    public async Task AddAsync_Success_ReturnHttpResponseCreatedWithExpectedData()
    {
        // Arrange
        AddFinancialMovementDto request = base.Fixture.Create<AddFinancialMovementDto>();
        Guid expectedId = Guid.NewGuid();
        string expectedUrl = base.Fixture.Create<string>();

        _appServiceMock.Setup(a => a.AddAsync(request)).ReturnsAsync(expectedId);
        _urlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>())).Returns(expectedUrl);

        // Act
        IResult response = await _controller.AddAsync(request);

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
        GetFinancialMovementDto expectedData = base.Fixture.Create<GetFinancialMovementDto>();

        _appServiceMock.Setup(a => a.GetByIdAsync(id)).ReturnsAsync(expectedData);

        // Act
        IResult response = await _controller.GetByIdAsync(id);

        // Assert
        Ok<ResponseData<GetFinancialMovementDto>> ok = response.Should().BeOfType<Ok<ResponseData<GetFinancialMovementDto>>>().Subject;
        ok.Value.Should().Match<ResponseData<GetFinancialMovementDto>>(a =>
            a.Data == expectedData &&
            a.ErrorMessage == null);
    }

    [Fact]
    public async Task UpdateAsync_Success_ReturnHttpResponseNoContent()
    {
        // Arrange
        Guid id = base.Fixture.Create<Guid>();
        JsonPatchDocument<UpdateFinancialMovementDto> jsonPathDocument = base.Fixture.Build<JsonPatchDocument<UpdateFinancialMovementDto>>()
            .Without(a => a.ContractResolver)
            .Create();

        // Act
        IResult response = await _controller.UpdateAsync(id, jsonPathDocument);

        // Assert
        response.Should().BeOfType<NoContent>();

        _appServiceMock.Verify(a => a.UpdateAsync(id, jsonPathDocument), Times.Once);
    }
}