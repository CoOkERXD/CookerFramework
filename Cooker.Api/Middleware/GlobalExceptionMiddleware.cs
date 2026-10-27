using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cooker.Api.Middleware
{
    public class GlobalExceptionMiddleware : IBaseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate _next, ILogger<GlobalExceptionMiddleware> logger)
        {
            this._logger = logger;
            this._next = _next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var traceId = context.TraceIdentifier;
                _logger.LogError(ex, "发生异常{message}", ex.Message);
                var response = context.Response;
                response.ContentType = "application/json";

                var payload = "{\"ret\":500,\"err\":\"系统内部发生错误。\"}";
                //自定义返回的数据类型
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await response.WriteAsync(payload);
            }
        }
    }
}