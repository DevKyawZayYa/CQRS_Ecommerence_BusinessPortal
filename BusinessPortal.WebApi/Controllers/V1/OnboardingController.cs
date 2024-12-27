using BusinessPortal.Application.UseCases.Users.Commands.LoginUser;
using BusinessPortal.Application.UseCases.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OnboardingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterUserCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("EmailLogIn")]
        public async Task<ActionResult> EmailLogInAsync([FromBody] LoginUserCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
