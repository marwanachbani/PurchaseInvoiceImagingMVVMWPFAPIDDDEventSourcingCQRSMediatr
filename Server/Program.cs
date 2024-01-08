using Core.Events;
using Core.Extensions;
using Data.SQLServer;
using Duende.IdentityServer.Validation;
using EventStore.Extensions;
using ImageStore.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PurchaseInvoiceCS.Extensions;
using PurchaseInvoiceQS.Queries;
using System.Reflection;
using UserCS.UserDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthentication();
DependencyInjection.GetDataServices(builder.Services);
AddCore.AddServices(builder.Services);
AddEventStore.AddServices(builder.Services);
AddUserCS.GetUserServices(builder.Services,builder.Configuration);
AddImageStore.AddServices(builder.Services);
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Program).GetTypeInfo().Assembly));
builder.Services.AddTransient<AppDbContext>();

AddPurchaseCS.AddServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");

        // Optionally, set the Swagger UI to require authentication
        c.OAuthClientId("swagger");
        c.OAuthClientSecret("swagger-secret");
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}
app.UseAuthentication();



app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var identityContext = services.GetRequiredService<IdentityAppContext>();
var userManager = services.GetRequiredService<UserManager<UserApp>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
   
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    
   
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
