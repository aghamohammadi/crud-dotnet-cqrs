using Microsoft.AspNetCore.Mvc;
using Mc2.CrudTest.Application.Features.Customer.Queries;
using Mc2.CrudTest.Application.Features.Customer.Commands.CreateCustomer;
using Mc2.CrudTest.Application.Features.Customer.Commands.DeleteCustomer;
using Mc2.CrudTest.Application.Features.Customer.Commands.UpdateCustomer;
using Mc2.CrudTest.Presentation.Shared.Dtos;
using MediatR;
using FluentValidation;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CustomerDto>> Get()
        {
            return await _mediator.Send(new GetCustomersQuery());
        }

        [HttpGet("{id:guid}")]
        public async Task<CustomerDto> Get(Guid id)
        {
            return await _mediator.Send(new GetCustomerByIdQuery { Id = id });
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                // Return a user-friendly validation error
                return BadRequest(new { Message = "Validation failed", Errors = ex.Message });
            }
            catch (Exception ex)
            {
                // General error handling
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            try
            {
                if (id != command.Id) 
                    return BadRequest();
                await _mediator.Send(command);
                return Ok();
            }
            catch (ValidationException ex)
            {
                // Return a user-friendly validation error
                return BadRequest(new { Message = "Validation failed", Errors = ex.Message });
            }
            catch (Exception ex)
            {
                // General error handling
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }

            
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCustomerCommand { Id = id });
            return NoContent();
        }


        [HttpGet("check-email")]
        public async Task<bool> CheckEmail([FromQuery] CheckUniqueEmailQuery model)
        {
            return await _mediator.Send(new CheckUniqueEmailQuery { Email = model.Email,Id = model.Id});
        }
    }
}
