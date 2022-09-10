using Application.Features.Technology.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technology.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommandValidator : AbstractValidator<UpdatedTechnologyDto>
    {
        public UpdateTechnologyCommandValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Name).MinimumLength(2);
            RuleFor(t => t.LanguageId).NotEmpty();
        }
    }
}
