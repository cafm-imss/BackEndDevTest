using Cafm.Application.Interfaces;
using Cafm.Application.Services;
using Cafm.Framework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSignalR();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IWorkOrderService, WorkOrderService>();
            return services;
        }
    }
}
