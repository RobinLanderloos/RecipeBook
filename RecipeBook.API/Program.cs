using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RecipeBook.API.Extensions;
using RecipeBook.API.ResponseHandlers;
using RecipeBook.API.Services;
using RecipeBook.Domain.Models;
using RecipeBook.Infrastructure.EntityFramework;
using RecipeBook.Infrastructure.Models.Dtos.IngredientLine;
using RecipeBook.Infrastructure.Models.Dtos.Recipe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContexts<RecipeBookContext, RecipeBookContext, User>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("RecipeBook")));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.ConfigureIdentity();

builder.Services.AddResponseHandlers();
builder.Services.AddServices();

builder.Services.AddLogging();
builder.Services.AddAutoMapper(typeof(RecipeBook.Infrastructure.Profiles.RecipeBook).Assembly);
builder.Services.AddMediatR(typeof(RecipeBookContext));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "RecipeBook.API", Version = "v1" });
    c.AddBearerTokenConfiguration();
});

PrepDb.SeedDatabase(builder.Services.BuildServiceProvider()).Wait();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipeBook.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
