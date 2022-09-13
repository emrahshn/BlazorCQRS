using Application.OrderOp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Stroopwafels.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var odds = await _mediator.Send(new GetOrdersQuery());
            return Ok(odds);
        }
        [HttpPost]
        public async Task<IActionResult> Post(AddEditOrdersCommand command)
        {
            var odds = await _mediator.Send(command);
            return Ok(odds);
        }
    }
}