using System.Text.Json.Serialization;
using AnimeWeb.Data;
using AnimeWeb.Mapper;
using AnimeWeb.Repository;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conectionString = builder.Configuration.GetConnectionString("DefaultConection");
builder.Services.AddDbContext<DataContext>(
    option => option.UseSqlite(conectionString) 
);
//Para ignorar ciclos repetitivos
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Repository
builder.Services.AddScoped<IAnimeRepository,AnimeRepository>();
builder.Services.AddScoped<IChapterRepository,ChapterRepository>();
builder.Services.AddScoped<IVideoRepository,VideoRepository>();

//Service
builder.Services.AddScoped<AnimeService>();
builder.Services.AddScoped<ChapterService>();
builder.Services.AddScoped<VideoService>();

//Mapeo de entidades y dto
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
