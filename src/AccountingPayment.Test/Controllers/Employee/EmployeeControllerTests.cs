using AccountingPayment.Application.UserCases.Employee.Commands;
using AccountingPayment.Application.UserCases.Employee.Querys;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.WepApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AccountingPayment.Test.Controllers.Employee
{
    public class EmployeeControllerTests
    {
        private readonly Mock<ISender> _mediatorMock;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mediatorMock = new Mock<ISender>();
            _controller = new EmployeeController(_mediatorMock.Object);
        }

        #region CreateEmployee
        [Fact]
        public void CreateEmployee_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var command = new EmployeeCreateRequest();
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseSuccess(new EmployeeResponse());
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.CreateEmployee(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public void CreateEmployee_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var command = new EmployeeCreateRequest();
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseError("Error", "Error");
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.CreateEmployee(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }
        #endregion

        #region UpdateEmployee
        [Fact]
        public void UpdateEmployee_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var command = new EmployeeUpdateRequest();
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseSuccess(new EmployeeResponse());
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.UpdateEmployee(command);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedResult, okResult.Value);
            _mediatorMock.Verify(x => x.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void UpdateEmployee_NotFound_ReturnsNotFound()
        {
            // Arrange
            var command = new EmployeeUpdateRequest();
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseError("Error", "NotFound");
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.UpdateEmployee(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public void UpdateEmployee_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var command = new EmployeeUpdateRequest();
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseError("", "");
            _mediatorMock.Setup(x => x.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.UpdateEmployee(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }
        #endregion

        #region DeleteEmployee
        [Fact]
        public void DeleteEmployee_ValidEmployeeId_ReturnsOkResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool>().ReponseSuccess(true);
            _mediatorMock.Setup(x => x.Send(It.IsAny<EmployeeDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.DeleteEmployee(employeeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedResult, okResult.Value);
            _mediatorMock.Verify(x => x.Send(It.IsAny<EmployeeDeleteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void DeleteEmployee_InvalidEmployeeId_ReturnsBadRequestResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool>().ReponseError("Invalid employee ID", "Invalid employee ID");
            _mediatorMock.Setup(x => x.Send(It.IsAny<EmployeeDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.DeleteEmployee(employeeId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal(expectedResult, badRequestResult.Value);
            _mediatorMock.Verify(x => x.Send(It.IsAny<EmployeeDeleteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void DeleteEmployee_NotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var expectedResult = new ApplicationResult<bool>().ReponseError("Employee NotFound", "NotFound");
            _mediatorMock.Setup(x => x.Send(It.IsAny<EmployeeDeleteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.DeleteEmployee(employeeId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.Equal(expectedResult, notFoundResult.Value);
            _mediatorMock.Verify(x => x.Send(It.IsAny<EmployeeDeleteCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetAllEmployee
        [Fact]
        public void GetAllEmployee_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var query = new EmployeeGetAllQuery();
            var expectedResult = new ApplicationResult<IEnumerable<EmployeeResponse>>().ReponseSuccess(new List<EmployeeResponse>());
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.GetAllEmployee();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedResult, okResult.Value);
            _mediatorMock.Verify(x => x.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void GetAllEmployee_NotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var query = new EmployeeGetAllQuery();
            var expectedResult = new ApplicationResult<IEnumerable<EmployeeResponse>>().ReponseError("Data not found", "NotFound");

            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.GetAllEmployee();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.Equal(expectedResult, notFoundResult.Value);
            _mediatorMock.Verify(x => x.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void GetAllEmployee_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var query = new EmployeeGetAllQuery();
            var expectedResult = new ApplicationResult<IEnumerable<EmployeeResponse>>().ReponseError("BadRequest", "Invalid request");
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.GetAllEmployee();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal(expectedResult, badRequestResult.Value);
            _mediatorMock.Verify(x => x.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

        #region GetByIdEmployee
        [Fact]
        public void GetByIdEmployee_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var query = new EmployeeGetByIdQuery(employeeId);
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseSuccess(new EmployeeResponse());
            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.GetByIdEmployee(employeeId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expectedResult, okResult.Value);
            _mediatorMock.Verify(x => x.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void GetByIdEmployee_NotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var query = new EmployeeGetByIdQuery(employeeId);
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseErrorFluentValidator(new List<ApplicationError>
            {
                new ApplicationError { Code = "NotFound", Description = "Data not found" }
            });

            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.GetByIdEmployee(employeeId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.Equal(expectedResult, notFoundResult.Value);
            _mediatorMock.Verify(x => x.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void GetByIdEmployee_InvalidRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var query = new EmployeeGetByIdQuery(employeeId);
            var expectedResult = new ApplicationResult<EmployeeResponse>().ReponseErrorFluentValidator(new List<ApplicationError>
            {
                new ApplicationError { Code = "BadRequest", Description = "Invalid request" }
            });

            _mediatorMock.Setup(x => x.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);

            // Act
            var result = _controller.GetByIdEmployee(employeeId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal(expectedResult, badRequestResult.Value);
            _mediatorMock.Verify(x => x.Send(query, It.IsAny<CancellationToken>()), Times.Once);
        }
        #endregion

    }
}
