using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testApp.Services;

namespace testApp
{
    public class FileMiddleware
    {
        private readonly RequestDelegate _next;
        FileService _fileService;
        public FileMiddleware(RequestDelegate next, FileService fileService)
        {
            _next = next;
            _fileService = fileService;
        }
         public async Task InvokeAsinc(HttpContext httpContext)
        {
            //i++;
            //httpContext.Response.ContentType = "text/html;charset=utf-8";
            //await httpContext.Response.WriteAsync($"Запрос {i}; Counter: {value.Value}; Service: {counterService.Value.Value}");
        }
    }
}
