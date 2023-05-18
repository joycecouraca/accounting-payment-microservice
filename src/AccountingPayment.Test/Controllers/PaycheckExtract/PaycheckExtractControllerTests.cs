using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.WepApi.Controllers;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AccountingPayment.Test.Controllers.PaycheckExtract
{
    public class PaycheckExtractControllerTests
    {
        private readonly ISender _mediatorFake;
        private readonly PaycheckExtractController _controller;

        public PaycheckExtractControllerTests()
        {
            _mediatorFake = A.Fake<ISender>();
            _controller = new PaycheckExtractController(_mediatorFake);
        }

        [Fact]
        public async Task PaycheckExtract_ValidRequest_ReturnsOk()
        {
            // Arrange
            var request = new PaycheckExtractRequest();
            var expectedResult = new ApplicationResult<PaycheckExtractResponse>().ReponseSuccess(new PaycheckExtractResponse());

            A.CallTo(() => _mediatorFake.Send(request, A<CancellationToken>._)).Returns(expectedResult);

            // Act
            var result = _controller.PaycheckExtract(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public async Task PaycheckExtract_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new PaycheckExtractRequest();
            var expectedResult = new ApplicationResult<PaycheckExtractResponse>().ReponseError("Error", "Error");

            A.CallTo(() => _mediatorFake.Send(request, A<CancellationToken>._)).Returns(expectedResult);

            // Act
            var result = _controller.PaycheckExtract(request) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public async Task PaycheckExtract_NotFound_ReturnsNotFound()
        {
            // Arrange
            var request = new PaycheckExtractRequest();
            var expectedResult = new ApplicationResult<PaycheckExtractResponse>().ReponseError("NotFound", "NotFound");

            A.CallTo(() => _mediatorFake.Send(request, A<CancellationToken>._)).Returns(expectedResult);

            // Act
            var result = _controller.PaycheckExtract(request) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }
    }
}
