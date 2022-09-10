using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommandValidator:AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(u => u.Id).NotEmpty();
            RuleFor(u => u.UserId).NotEmpty();
            RuleFor(u => u.ProfileUrl).NotEmpty();
        }
    }
}
