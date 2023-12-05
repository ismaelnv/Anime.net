using System.Text.Json.Serialization;
using AnimeWeb.Data;
using AnimeWeb.Mapper;
using AnimeWeb.Repository;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service;
using AnimeWeb.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

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
builder.Services.AddScoped<IGenreRepository,GenreRepository>();
builder.Services.AddScoped<IStudioRepository,StudioRepository>();



//Service
builder.Services.AddScoped<IAnimeService,AnimeService>();
builder.Services.AddScoped<IChapterService,ChapterService>();
builder.Services.AddScoped<IVideoService,VideoService>();
builder.Services.AddScoped<IGenreService,GenreService>();
builder.Services.AddScoped<IStudioService,StudioService>();

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

app.UseStaticFiles(new StaticFileOptions(){

    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath,"Images")),
    RequestPath = "/img"
});

app.Run();
