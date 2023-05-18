using AccountingPayment.Application.UserCases.Sector.Commands;
using AccountingPayment.Application.UserCases.Sector.Querys;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Request;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.WepApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AccountingPayment.Test.Controllers.Sector
{
    public class SectorControllerTests
    {
        private readonly SectorController _sectorController;
        private readonly Mock<ISender> _mediatorMock;

        public SectorControllerTests()
        {
            _mediatorMock = new Mock<ISender>();
            _sectorController = new SectorController(_mediatorMock.Object);
        }

        #region CreateSector
        [Fact]
        public void CreateSector_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var command = new SectorCreateRequest();
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseSuccess(new SectorResponse());
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.CreateSector(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedResult, okResult.Value);
            _mediatorMock.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void CreateSector_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var command = new SectorCreateRequest();
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseErrorFluentValidator(new List<ApplicationError>
            {
                new ApplicationError { Code = "BadRequest", Description = "Invalid request" }
            });
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.CreateSector(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal(expectedResult, badRequestResult.Value);
            _mediatorMock.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region UpdateSector
        [Fact]
        public void UpdateSector_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var command = new SectorUpdateRequest();
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseSuccess(new SectorResponse());
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.UpdateSector(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedResult, okResult.Value);
            _mediatorMock.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void UpdateSector_InvalidRequest_ReturnsNotFoundResult()
        {
            // Arrange
            var command = new SectorUpdateRequest();
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseErrorFluentValidator(new List<ApplicationError>
            {
                new ApplicationError { Code = "NotFound", Description = "Sector not found" }
            });
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.UpdateSector(command);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.Equal(expectedResult, notFoundResult.Value);
            _mediatorMock.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void UpdateSector_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var command = new SectorUpdateRequest();
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseErrorFluentValidator(new List<ApplicationError>
            {
                new ApplicationError { Code = "BadRequest", Description = "Invalid request" }
            });

            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.UpdateSector(command);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal(expectedResult, badRequestResult.Value);
            _mediatorMock.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region DeleteSector
        [Fact]
        public void DeleteSector_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool>().ReponseSuccess(true);
            _mediatorMock.Setup(x => x.Send(It.IsAny<SectorDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.DeleteSector(sectorId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public void DeleteSector_NotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool>().ReponseError("NotFound", "NotFound");
            _mediatorMock.Setup(x => x.Send(It.IsAny<SectorDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.DeleteSector(sectorId) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public void DeleteSector_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool>().ReponseError("InvalidRequest", "InvalidRequest");
            _mediatorMock.Setup(x => x.Send(It.IsAny<SectorDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.DeleteSector(sectorId) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }
        #endregion

        #region GetAllSector
        [Fact]
        public void GetAllSector_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var query = new SectorGetAllQuery();
            var expectedResult = new ApplicationResult<IEnumerable<SectorResponse>>().ReponseSuccess(new List<SectorResponse>());
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.GetAllSector();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public void GetAllSector_ReturnsNotFound_WhenResultIsNotFound()
        {
            // Arrange
            var query = new SectorGetAllQuery();
            var error = new ApplicationError("NotFound", "Sector not found.");
            var errors = new List<ApplicationError>() { error };
            var expectedResult = new ApplicationResult<IEnumerable<SectorResponse>>().ReponseErrorFluentValidator(errors);
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.GetAllSector();
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.Equal(expectedResult, notFoundResult.Value);
        }

        [Fact]
        public void GetAllSector_ReturnsBadRequest_WhenResultIsFailure()
        {
            // Arrange
            var query = new SectorGetAllQuery();
            var error = new ApplicationError("Validation", "Invalid input.");
            var errors = new List<ApplicationError>() { error };
            var expectedResult = new ApplicationResult<IEnumerable<SectorResponse>>().ReponseErrorFluentValidator(errors);
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.GetAllSector();
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal(expectedResult, badRequestResult.Value);
        }
        #endregion

        #region GetByIdSector
        [Fact]
        public void GetByIdSector_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var query = new SectorGetByIdQuery(sectorId);
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseSuccess(new SectorResponse());
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.GetByIdSector(sectorId);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public void GetByIdSector_ReturnsNotFound_WhenResultIsNotFound()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var query = new SectorGetByIdQuery(sectorId);
            var error = new ApplicationError("NotFound", "Sector not found.");
            var errors = new List<ApplicationError>() { error };
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseErrorFluentValidator(errors);
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.GetByIdSector(sectorId);
            var notFoundResult = result as NotFoundObjectResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            Assert.Equal(expectedResult, notFoundResult.Value);
        }

        [Fact]
        public void GetByIdSector_ReturnsBadRequest_WhenResultIsFailure()
        {
            // Arrange
            var sectorId = Guid.NewGuid();
            var query = new SectorGetByIdQuery(sectorId);
            var error = new ApplicationError("Validation", "Invalid input.");
            var errors = new List<ApplicationError>() { error };
            var expectedResult = new ApplicationResult<SectorResponse>().ReponseErrorFluentValidator(errors);
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _sectorController.GetByIdSector(sectorId);
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.Equal(expectedResult, badRequestResult.Value);
        }
        #endregion
    }
}
