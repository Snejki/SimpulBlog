using Microsoft.AspNetCore.Builder;
using SimpulBlog.API.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpulBlog.API.Extensions
{
    public static class GlobalExceptionExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
