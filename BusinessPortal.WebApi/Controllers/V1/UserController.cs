using BusinessPortal.Application.UseCases.Users.Commands.CreateUserCommand;
using BusinessPortal.Application.UseCases.Users.Commands.DeleteUserCommand;
using BusinessPortal.Application.UseCases.Users.Commands.UpdateUserCommand;
using BusinessPortal.Application.UseCases.Users.Queries.GetAllUserQuery;
using BusinessPortal.Application.UseCases.Users.Queries.GetAllWithPaginationUserQuery;
using BusinessPortal.Application.UseCases.Users.Queries.GetByIdUserQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllUserQuery());
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync([FromQuery] Guid UserId)
        {
            var response = await _mediator.Send(new GetByIdUserQuery() { UserId = UserId });
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] GetAllWithPaginationUserQuery query)
        {
            var response = await _mediator.Send(query);
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] CreateUserCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateUserCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteAsync([FromQuery] DeleteUserCommand command)
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
