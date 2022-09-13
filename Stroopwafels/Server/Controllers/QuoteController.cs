using Application.QuoteOp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Stroopwafels.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        IMediator _mediator;
        public QuoteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Get(GetQuotesQueryRequest request)
        {
            var odds = await _mediator.Send(new GetQuotesQuery(request));
            return Ok(odds);
        }
    }
}