using Authlib.Database.Models;
using AuthLib.Database;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace AuthLib.Extentions.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _firebaseProjectId;

    public AuthenticationMiddleware(RequestDelegate next, string firebaseProjectId)
    {
        _next = next;
        _firebaseProjectId = firebaseProjectId;
    }

    public async Task InvokeAsync(HttpContext context, AuthDbContext dbContext)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValues))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        var authorizationHeaderValue = authorizationHeaderValues.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authorizationHeaderValue) || !authorizationHeaderValue.StartsWith("Bearer "))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        var token = authorizationHeaderValue.Substring("Bearer ".Length);

        FirebaseApp firebaseApp = FirebaseApp.DefaultInstance;
        if (firebaseApp == null)
        {

            try
            {
                firebaseApp = FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("./google_creds.json"),
                    ProjectId = _firebaseProjectId
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return;
            }
        }

        var firebaseAuth = FirebaseAuth.DefaultInstance;
        FirebaseToken firebaseToken;
        try
        {
            firebaseToken = await firebaseAuth.VerifyIdTokenAsync(token);
        }
        catch (FirebaseAuthException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }
;
        var firebaseId = firebaseToken.Subject;

        User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == firebaseId);

        if (user == null)
        {

            // tODO throw custom error
            Assembly.GetExecutingAssembly().GetName();
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        context.Items["User"] = user;
        await _next(context);
    }
}


