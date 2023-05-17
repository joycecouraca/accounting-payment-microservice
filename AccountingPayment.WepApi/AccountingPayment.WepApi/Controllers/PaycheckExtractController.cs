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
    public class PaycheckExtractController : ApiControllerBase
    {
        public PaycheckExtractController(ISender mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<EmployeeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status404NotFound)]

        public IActionResult CreateEmployee([FromBody] PaycheckExtractRequest command)
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(command);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }
    }
}
