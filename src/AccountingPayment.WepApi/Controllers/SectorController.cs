using AccountingPayment.Application.UserCases.Sector.Commands;
using AccountingPayment.Application.UserCases.Sector.Querys;
using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Sector.Request;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.WepApi.Configuration.ApiBase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingPayment.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ApiControllerBase
    {
        public SectorController(ISender mediator) : base(mediator)
        {
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<SectorResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status404NotFound)]

        public IActionResult CreateSector([FromBody] SectorCreateRequest command)
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
        [ProducesResponseType(typeof(ApplicationResult<SectorResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status404NotFound)]

        public IActionResult UpdateSector([FromBody] SectorUpdateRequest command)
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

        [HttpDelete("{sectorId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<bool?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApplicationResult<bool?>), StatusCodes.Status404NotFound)]

        public IActionResult DeleteSector([FromRoute] Guid sectorId)
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(new SectorDeleteCommand(sectorId));

                if (!result.Success && result.Errors!.Any(x => x.Code!.Equals("NotFound")))
                    return NotFound(result);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<List<string?>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<List<string?>>), StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSector()
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(new SectorGetAllQuery());

                if (!result.Success && result.Errors!.Any(x => x.Code!.Equals("NotFound")))
                    return NotFound(result);
                else if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }

        [HttpGet("{sectorId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApplicationResult<string?>), StatusCodes.Status400BadRequest)]
        public IActionResult GetByIdSector([FromRoute] Guid sectorId)
        {
            return Execute(async () =>
            {
                var result = await _mediator.Send(new SectorGetByIdQuery(sectorId));

                if (!result.Success && result.Errors!.Any(x => x.Code!.Equals("NotFound")))
                    return NotFound(result);
                else if (!result.Success)
                    return BadRequest(result);

                return Ok(result);
            });
        }
    }
}
