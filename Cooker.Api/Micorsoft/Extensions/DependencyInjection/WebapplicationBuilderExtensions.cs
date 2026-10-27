using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebapplicationBuilderExtensions
    {
        public static WebApplicationBuilder InitApplication(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddServices(builder.Configuration);
            return builder;
        }
    }
}