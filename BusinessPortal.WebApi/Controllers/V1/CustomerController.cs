﻿using BusinessPortal.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using BusinessPortal.Application.UseCases.Customers.Commands.DeleteCustomerCommand;
using BusinessPortal.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using BusinessPortal.Application.UseCases.Customers.Queries.GetAllCustomerQuery;
using BusinessPortal.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery;
using BusinessPortal.Application.UseCases.Customers.Queries.GetByIdCustomerQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessPortal.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllCustomerQuery());
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync([FromQuery] Guid customerId)
        {
            var response = await _mediator.Send(new GetByIdCustomerQuery() { CustomerId = customerId });
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] GetAllWithPaginationCustomerQuery query)
        {
            var response = await _mediator.Send(query);
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> InsertAsync([FromBody] CreateCustomerCommand command)
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
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCustomerCommand command)
        {
            if (command is null) return BadRequest();

            var response = await _mediator.Send(command);

            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("Delete")]
        public async Task<ActionResult> DeleteAsync([FromQuery] DeleteCustomerCommand command)
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
