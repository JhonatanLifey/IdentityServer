using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MoviesAPIContext>(options =>
    options.UseInMemoryDatabase("Movies"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


SeedDatabase(app);


static void SeedDatabase(IHost app)
{
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	var movieContext = services.GetRequiredService<MoviesAPIContext>();
	MoviesAPIContextSeed.SeedAsync(movieContext);
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
