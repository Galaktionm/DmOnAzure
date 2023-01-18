using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Entities;
using UserService.Commands;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/userservice/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userService;

        private readonly JWTService jWTService;

        private readonly IMediator mediator;

        public AccountController(UserManager<User> userService, JWTService jWTService, IMediator mediator)
        {
            this.userService = userService;
            this.jWTService = jWTService;
            this.mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestCommand request)
        {
            try
            {
                await mediator.Send(request);
                return Ok(new { message="Registration successful"});
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestCommand request)
        {

            try
            {
                var result = await mediator.Send(request);
                return Ok(result);
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
