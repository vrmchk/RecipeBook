using RecipeBook.BLL.Profiles;
using RecipeBook.BLL.Services;
using RecipeBook.BLL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(config => config.AddProfiles(new[] {new RecipeProfile()}));
builder.Services.AddSingleton<IRecipeSearchService, RecipeSearchService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
