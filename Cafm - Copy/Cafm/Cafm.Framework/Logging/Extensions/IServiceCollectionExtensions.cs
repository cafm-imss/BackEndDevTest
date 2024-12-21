using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Framework.Logging.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddExceptionLogging(this IServiceCollection services)
        {
            services.ConfigureOptions<ExceptionLogOptions>();
        }
    }
}
