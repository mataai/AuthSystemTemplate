using AuthLib.DataContracts.ReponseUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace AuthLib.Extentions.Middlewares;
public static class ErrorHandlingExtention
{
    public static void UseErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";

                if (contextFeature.Error is IError contextEr)
                {
                    context.Response.StatusCode = (int)contextEr.HttpErrorCode;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(contextEr, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var errorResponse = new CustomErrorException
                    (
                        HttpStatusCode.InternalServerError,
                        "uknown",
                        "An unexpected error occurred",
                        contextFeature.Error.StackTrace ?? ""
                    ); ;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                }
            });
        });
    }
}

