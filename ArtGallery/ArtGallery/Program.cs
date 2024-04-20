using ArtGallery.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ArtGalleryApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ArtGalleryApiContext") ?? throw new InvalidOperationException("Connection string 'ArtGalleryApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
