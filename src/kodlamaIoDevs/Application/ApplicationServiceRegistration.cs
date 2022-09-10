
using Application.Features.Accounts.Rules;
using Application.Features.Auth.Commands.AuthLogin;
using Application.Features.Languages.Rules;
using Application.Features.Technology.Rules;
using Core.Application.Pipelines.Validation;
using FluentAssertions.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<LanguageBusinessRules>();
            services.AddScoped<TechnologyBusinessRules>();
            services.AddScoped<AccountBusinessRules>();
            services.AddScoped<AuthBusinessRules>();

            return services;
        }
    }
}
