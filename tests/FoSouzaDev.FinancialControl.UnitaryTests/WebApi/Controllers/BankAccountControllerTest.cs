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

public class BankAccountControllerTest : BaseTest
{
    private readonly Mock<IBankAccountAppService> _appServiceMock;
    private readonly Mock<IUrlHelper> _urlHelper;

    private readonly BankAccountController _controller;

    public BankAccountControllerTest()
    {
        _appServiceMock = new Mock<IBankAccountAppService>();
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
        AddBankAccountDto request = base.Fixture.Create<AddBankAccountDto>();
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
        GetBankAccountDto expectedData = base.Fixture.Create<GetBankAccountDto>();

        _appServiceMock.Setup(a => a.GetByIdAsync(id)).ReturnsAsync(expectedData);

        // Act
        IResult response = await _controller.GetByIdAsync(id);

        // Assert
        Ok<ResponseData<GetBankAccountDto>> ok = response.Should().BeOfType<Ok<ResponseData<GetBankAccountDto>>>().Subject;
        ok.Value.Should().Match<ResponseData<GetBankAccountDto>>(a =>
            a.Data == expectedData &&
            a.ErrorMessage == null);
    }

    [Fact]
    public async Task UpdateAsync_Success_ReturnHttpResponseNoContent()
    {
        // Arrange
        Guid id = base.Fixture.Create<Guid>();
        JsonPatchDocument<UpdateBankAccountDto> jsonPathDocument = base.Fixture.Build<JsonPatchDocument<UpdateBankAccountDto>>()
            .Without(a => a.ContractResolver)
            .Create();

        // Act
        IResult response = await _controller.UpdateAsync(id, jsonPathDocument);

        // Assert
        response.Should().BeOfType<NoContent>();

        _appServiceMock.Verify(a => a.UpdateAsync(id, jsonPathDocument), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_Success_ReturnHttpResponseNoContent()
    {
        // Arrange
        Guid id = base.Fixture.Create<Guid>();

        // Act
        IResult response = await _controller.RemoveAsync(id);

        // Assert
        response.Should().BeOfType<NoContent>();

        _appServiceMock.Verify(a => a.RemoveAsync(id), Times.Once);
    }
}