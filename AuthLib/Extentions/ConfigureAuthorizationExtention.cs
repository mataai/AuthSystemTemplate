using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AuthLib.Extentions;
public static class ConfigureAuthorizationExtention
{
    public static void ConfigureAuthorization(this IServiceCollection services, bool isProduction)
    {
        services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var issuer = $"https://securetoken.google.com/skiapp-bd9b5";
            options.Authority = issuer;
            options.TokenValidationParameters.ValidAudience = "skiapp-bd9b5";
            options.TokenValidationParameters.ValidIssuer = issuer;
            options.TokenValidationParameters.ValidateIssuer = isProduction;
            options.TokenValidationParameters.ValidateAudience = isProduction;
            options.TokenValidationParameters.ValidateLifetime = isProduction;
            options.TokenValidationParameters.RequireSignedTokens = isProduction;

            if (isProduction)
            {
                var jwtKeySetUrl = "https://www.googleapis.com/robot/v1/metadata/x509/securetoken@system.gserviceaccount.com";
                options.TokenValidationParameters.IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                {
                    // get JsonWebKeySet from AWS
                    var keyset = new HttpClient()
                        .GetFromJsonAsync<Dictionary<string, string>>(jwtKeySetUrl).Result;

                    // serialize the result
                    var keys = keyset!.Values.Select(d => new X509SecurityKey(new X509Certificate2(Encoding.UTF8.GetBytes(d))));

                    // cast the result to be the type expected by IssuerSigningKeyResolver
                    return keys;
                };
            }
        });

        services.AddAuthorization();
    }
}
