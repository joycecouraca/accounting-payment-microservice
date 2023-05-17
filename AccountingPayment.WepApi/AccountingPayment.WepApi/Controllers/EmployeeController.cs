using AccountingPayment.Application.UserCases.Employee.Commands;
using AccountingPayment.Application.UserCases.Employee.Querys;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.WepApi.Configuration.ApiBase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingPayment.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ApiControllerBase
    {
        public EmployeeController(ISender mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status404NotFound)]

        public IActionResult CreateEmployee([FromBody] EmployeeCreateRequest command)
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(command);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status404NotFound)]

        public IActionResult UpdateEmployee([FromBody] EmployeeUpdateRequest command)
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(command);

                if (!result.Success && result.Errors!.Any(x => x.Code!.Equals("NotFound")))
                    return NotFound(result);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

        [HttpDelete("{employeeId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status404NotFound)]

        public IActionResult DeleteEmployee([FromRoute] Guid employeeId)
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(new EmployeeDeleteCommand(employeeId));

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        public IActionResult GetAllEmployee()
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(new EmployeeGetAllQuery());

                if (!result.Success && result.Errors!.Any(x => x.Code!.Equals("NotFound")))
                    return NotFound(result);
                else if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

        [HttpGet("{employeeId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        public IActionResult GetByIdEmployee([FromRoute] Guid employeeId)
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(new EmployeeGetByIdQuery(employeeId));

                if (!result.Success && result.Errors!.Any(x => x.Code!.Equals("NotFound")))
                    return NotFound(result);
                else if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

    }
}
