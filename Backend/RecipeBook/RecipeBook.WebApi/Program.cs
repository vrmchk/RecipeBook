using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeBook.BLL.Configure;
using RecipeBook.BLL.Extensions;
using RecipeBook.BLL.Filters;
using RecipeBook.BLL.Profiles;
using RecipeBook.BLL.Services;
using RecipeBook.BLL.Services.Interfaces;
using RecipeBook.DAL.Context;
using RecipeBook.DAL.Repositories;
using RecipeBook.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfig>(config => config.Secret = builder.Configuration["Secrets:JwtConfig"]);
builder.Services.AddAutoMapper(config => config.AddProfiles(new Profile[] {new RecipeProfile(), new RecipeSearchProfile()}));
builder.Services.AddDbContext<ApplicationContext>(options => {
    options.UseSqlite(builder.Configuration["Secrets:ConnectionString"]);
});
builder.Services.AddScoped<IRecipeRepo, RecipeRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IRecipeSearchService, RecipeSearchService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRecipesService, UserRecipesService>();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers(config => config.Filters.Add(new CustomExceptionFilterAttribute(builder.Environment)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();