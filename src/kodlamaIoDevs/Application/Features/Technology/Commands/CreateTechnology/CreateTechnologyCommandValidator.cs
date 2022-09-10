using Application.Features.Technology.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technology.Commands.CreateTechnology
{
    public class CreateTechnologyCommandValidator:AbstractValidator<CreatedTechnologyDto>
    {
        public CreateTechnologyCommandValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Name).MinimumLength(2);
            RuleFor(t => t.LanguageId).NotEmpty();
        }
    }
}
