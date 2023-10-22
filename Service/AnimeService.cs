using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Service
{
    public class AnimeService
    {
        private IAnimeRepository _animeRepository;
        private IChapterRepository _chapterRepository;

        public AnimeService(IAnimeRepository animeRepository, IChapterRepository chapterRepository)
        {
            _animeRepository = animeRepository;
            _chapterRepository = chapterRepository;
        }
        
        public async Task<IEnumerable<AnimeModel>> getAnimes(){
            return await _animeRepository.GetAll();
        }

        public async Task<CreateAnimeDto> createAnime(CreateAnimeDto animeDto){
            
            if (animeDto == null){

                return null;
            }

            AnimeModel anime = new()
            {
                name = animeDto.name,
                state = animeDto.state,
                description = animeDto.description
            };

            await _animeRepository.Create(anime);
            return animeDto;
        }

        public async Task<UpdateAnimeDto> updateAnime(int id, [FromBody] UpdateAnimeDto animeDto){

            if (id != animeDto.Id)
            {
                return null;
            }

            AnimeModel anime = new()
            {
                Id = animeDto.Id,
                name = animeDto.name,
                state = animeDto.state,
                description = animeDto.description
            };

            await _animeRepository.Update(anime);
            return animeDto;
        }

        public async Task<AnimeModel> getAnimeId(int id){

            AnimeModel anime = await _animeRepository.Obtain(A => A.Id == id);   
            return anime;
        }

        public async Task<AnimeModel> removeAnime(int id){

            if (id == 0)
            {
                return null;
            }

            AnimeModel anime = await _animeRepository.Obtain(A => A.Id == id);

            if (anime == null)
            {
                return null;
            }

            await _animeRepository.Remove(anime);
            return anime;
        }

        public async Task<AnimeModel> getAnimeChapters(int id){
            
            if (id == 0)
            {
                return null;
            }

            AnimeModel anime = await _animeRepository.getanimeChapters(id);
            return anime;
        }

        public async Task<AnimeModel> createAChapterAndRelateItToAnime(int id, CreateChapterDto chapterDto)
        {
            if (id == 0 || chapterDto == null)
            {
                return null;
            }

            AnimeModel anime = await this.getAnimeId(id);

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

            await _chapterRepository.Create(chapter);

            return anime;
        }

    }
}