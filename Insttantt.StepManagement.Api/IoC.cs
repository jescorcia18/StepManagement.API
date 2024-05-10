using Insttantt.StepManagement.Application.Common.Utils;
using Insttantt.StepManagement.Application.Common.Interfaces.Utils;
using Insttantt.StepManagement.Application.Common.Interfaces.Repository;
using Insttantt.StepManagement.Infrastructure.Repositories;
using Insttantt.StepManagement.Application.Common.Interfaces.Services;
using Insttantt.StepManagement.Application.Services;

namespace Insttantt.StepManagement.Api
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IUtility, Utility>();
            services.AddScoped<IStepService, StepService>();
            services.AddScoped<IStepRepository, StepRepository>();
            services.AddScoped<IStepFieldsService, StepFieldsService>();
            services.AddScoped<IStepFieldsRepository, StepFieldsRepository>();
            services.AddCors();
            return services;
        }
    }
}
