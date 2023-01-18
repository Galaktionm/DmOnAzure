using Aggregator.Commands;
using Aggregator.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserProto;

namespace Aggregator.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAccount(string userId)
        {
            var userCommand = new AggregatorAccountPageCommand(userId);
            var userResult = await mediator.Send(userCommand);
            if (!string.IsNullOrEmpty(userResult.username))
            {
                var orderCommand=new OrderAggregateCommand(userId);
                var orderResult=await mediator.Send(orderCommand);
                var orderAggregate = new AccountAggregate(userResult.email, userResult.username, userResult.balance, orderResult);
                return Ok(orderAggregate);
            }
            return BadRequest($"User with the id of {userId} not found");
        }
    }
}
