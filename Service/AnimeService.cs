using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Service
{
    public class AnimeService
    {
        private IAnimeRepository _animeRepository;
        private IChapterRepository _chapterRepository;
        private IMapper _mapper;

        public AnimeService(IAnimeRepository animeRepository, IChapterRepository chapterRepository, IMapper mapper)
        {
            _animeRepository = animeRepository;
            _chapterRepository = chapterRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<AnimeDto>> getAnimes(){

            IEnumerable<AnimeModel> animeList = await _animeRepository.GetAllAsync();
            IEnumerable<AnimeDto> animes = _mapper.Map<IEnumerable<AnimeDto>>(animeList);
            return animes;
        }

        public async Task<AnimeDto> createAnime(CreateAnimeDto createAnimeDto){
            
            if (createAnimeDto == null){
                return null;
                //throw new Exception("Anime no encontrado");
            }

           AnimeModel anime = _mapper.Map<AnimeModel>(createAnimeDto);

           await _animeRepository.CreateAsync(anime);

           AnimeDto animeDto1 = _mapper.Map<AnimeDto>(anime);
           return animeDto1;
        }

        public async Task<AnimeDto> updateAnime(int id, [FromBody] UpdateAnimeDto updateAnimeDto){

            if (id != updateAnimeDto.Id)
            {
                return null;
            }

            AnimeModel anime = _mapper.Map<AnimeModel>(updateAnimeDto);

            AnimeModel animeModel = await _animeRepository.UpdateAsync(anime);

            if (animeModel == null)
            {
                return null;
            }

            AnimeDto animeDto = _mapper.Map<AnimeDto>(animeModel);

            return animeDto;
        }

        public async Task<AnimeDto> getAnimeId(int id){

            if (id == 0)
            {
                return null;
            }

            AnimeModel anime = await _animeRepository.ObtainAsync(A => A.Id == id); 

            if (anime == null)
            {
                return null;
            }

            AnimeDto animeDto = _mapper.Map<AnimeDto>(anime);
            return animeDto ;
        }

        public async Task<AnimeDto> removeAnime(int id){

            if (id == 0)
            {
                return null;
            }

            AnimeModel anime = await _animeRepository.ObtainAsync(A => A.Id == id);

            if (anime == null)
            {
                return null;
            }

            await _animeRepository.RemoveAsync(anime);
            
            AnimeDto animeDto = _mapper.Map<AnimeDto>(anime);
            return animeDto;
        }

        public async Task<AnimeModel> getAnimeChapters(int id){
            
            if (id == 0)
            {
                return null;
            }

            AnimeModel anime = await _animeRepository.getanimeChaptersAsync(id);
            return anime;
        }

        public async Task<AnimeModel> createAChapterAndRelateItToAnime(int id, CreateChapterDto chapterDto)
        {
            if (id == 0 || chapterDto == null)
            {
                return null;
            }

            AnimeModel anime = await _animeRepository.ObtainAsync(A => A.Id == id);

            if (anime == null)
            {
                return null;
            }    

            ChapterModel chapter = new()
            {
                title = chapterDto.title,
                episode = chapterDto.episode,
                description = chapterDto.description,
                AnimeModel = anime 
            };

            await _chapterRepository.CreateAsync(chapter);

            return anime;
        }

    }
}