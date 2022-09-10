using Application.Features.Accounts.Commands.CreateAccount;
using Application.Features.Accounts.Commands.DeleteAccount;
using Application.Features.Accounts.Commands.UpdateAccount;
using Application.Features.Accounts.Dtos;
using Application.Features.Accounts.Models;
using Application.Features.Accounts.Queries.GetByIdAccount;
using Application.Features.Accounts.Queries.GetListAccount;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdAccountQuery getByIdAccountQuery)
        {
            AccountGetByIdDto? result = await Mediator.Send(getByIdAccountQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListAccountQuery getListAccountQuery = new() { PageRequest = pageRequest };
            AccountListModel result = await Mediator.Send(getListAccountQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAccountCommand createAccountCommand)
        {
            CreatedAccountDto? createdAccountDto = await Mediator.Send(createAccountCommand);
            return Created("", createdAccountDto);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] DeleteAccountCommand deleteAccountCommand)
        {
            DeletedAccountDto? result =
                await Mediator.Send(deleteAccountCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            [FromBody] UpdateAccountCommand updateAccountCommand)
        {
            UpdatedAccountDto? updatedAccountbDto =
                await Mediator.Send(updateAccountCommand);
            return Ok(updatedAccountbDto);
        }
    }
}
