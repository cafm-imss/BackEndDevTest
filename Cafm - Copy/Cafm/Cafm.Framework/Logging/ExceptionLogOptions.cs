using Cafm.Framework.Logging.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Framework.Logging
{
    public class ExceptionLogOptions : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.Filters.Add<ExceptionLogFilter>();
        }
    }
}
