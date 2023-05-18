using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountingPayment.WepApi.Configuration.ApiBase
{
    [ApiController]
    [Route("api")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
        protected ISender _mediator;
        public ApiControllerBase(ISender mediator)
        {
            _mediator = mediator;
        }

        [NonAction]
        public IActionResult Execute(Func<Task<IActionResult>> func)
        {
            try
            {
                var result = func().Result;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
