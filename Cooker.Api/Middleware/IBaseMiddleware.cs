using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Cooker.Api.Middleware
{
    public interface IBaseMiddleware
    {
        public abstract Task InvokeAsync(HttpContext context);
    }
}