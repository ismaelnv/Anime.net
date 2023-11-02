using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AutoMapper;

namespace AnimeWeb.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            
            CreateMap<AnimeModel,AnimeDto>().ReverseMap();
            CreateMap<AnimeModel, CreateAnimeDto>().ReverseMap();
            CreateMap<AnimeModel, UpdateAnimeDto>().ReverseMap();

            CreateMap<ChapterModel, ChapterDto>().ReverseMap();
            CreateMap<ChapterModel, CreateChapterDto>().ReverseMap();
            CreateMap<ChapterModel, UpdateChapterDto>().ReverseMap();  

            CreateMap<VideoModel, VideoDto>().ReverseMap();
            CreateMap<VideoModel, CreateVideoDto>().ReverseMap();
            CreateMap<VideoModel, UpdateVideoDto>().ReverseMap();   

            CreateMap<GenreModel, GenreDto>().ReverseMap();
            CreateMap<GenreModel, CreateGenreDto>().ReverseMap();
            CreateMap<GenreModel, updateGenreDto>().ReverseMap();
        }
        
    }
}