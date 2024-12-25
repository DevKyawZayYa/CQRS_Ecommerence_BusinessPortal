using BusinessPortal.Persistence;
using BusinessPortal.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BusinessPortal.Persistence.Contexts;
using BusinessPortal.Infrastructure;
using BusinessPortal.Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add persistence layer dependencies
builder.Services.AddSingleton<DapperContext>();

// Register repositories
builder.Services.ConfigureRepositories();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCustomerHandler).Assembly));

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
