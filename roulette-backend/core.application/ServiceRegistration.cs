using core.application.Common;
using core.application.interfaces;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Roulette.Application.Roulette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace core.application
{
    public static class ServiceRegistration
    {

        public static void AddApplicationLayer(this IServiceCollection svc)
        {
            svc.AddTransient<IRouletteService, RouletteService>();
            svc.AddTransient<IRandomProvider, RandomProvider>();

            svc.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
