using AuthLib.Database;
using AuthLib.DataContracts.MappingProfiles;
using AuthLib.Extentions;
using AuthLib.Extentions.Middlewares;
using Core.MappingProfiles.Users;
using UserManagmentSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCorsPolicy();
builder.Services.ConfigureAuthorization(false);
builder.Services.ConfigureApiBehavior();

builder.Services.AddAutoMapper(typeof(RoleToDtoProfile), typeof(UserToDtoProfile), typeof(RoleToDtoProfile));
builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<PermissionsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
if (!builder.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseErrorHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();