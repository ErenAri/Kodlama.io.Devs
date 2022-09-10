using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Commands.CreateAccount
{
    public class CreateAccountCommandValidator: AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(c=>c.UserId).NotEmpty();
            RuleFor(c => c.ProfileUrl).NotEmpty();
        }
    }
}
