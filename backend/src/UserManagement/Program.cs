using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using System.Reflection;
using UserManagement.Features.UserManagement.Validators;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.AppDbContexts;
using UserManagement.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// add PostgreSQL dbcontext
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// add repositories
builder.Services.AddRepositories();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()      // Allow all origins
            .AllowAnyMethod()      // Allow all HTTP methods (GET, POST, etc.)
            .AllowAnyHeader();     // Allow all headers
    });
});

// add mediatR and fluentvalidators
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
//builder.Services.AddValidatorsFromAssembly(Pro);
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Auto-migrate on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
