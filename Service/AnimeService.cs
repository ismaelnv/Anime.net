using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AnimeWeb.Service
{
    public class AnimeService
    {
        private IAnimeRepository _animeRepository;
        private DataContext _bd;

        public AnimeService(IAnimeRepository animeRepository,DataContext bd)
        {
            _animeRepository = animeRepository;
            _bd = bd;
        }
        
        public async Task<IEnumerable<AnimeModel>> getAnimes(){
            return await _animeRepository.GetAll();
        }

        public async Task<AnimeModel> createAnime(AnimeModel anime){
            
            if (anime == null){

                return null;
            }

            await _animeRepository.Create(anime);
            return anime;
        }

        public async Task<AnimeModel> updateAnime(int id, [FromBody] AnimeModel anime){

            AnimeModel animeUpdate = await _animeRepository.Update(anime);
            return animeUpdate;
        }

        public async Task<AnimeModel> getAnimeId(int id){

            var anime = await _animeRepository.Obtain(A => A.Id == id);   
            return anime;
        }

        public async Task<AnimeModel> removeAnime(int id){

            var anime = await _animeRepository.Obtain(A => A.Id == id);

            await _animeRepository.Remove(anime);
            return anime;
        }

        public async Task<AnimeModel> getAnimeChapters(int id){
            AnimeModel anime = await _animeRepository.getanimeChapters(id);
            return anime;
        }

    }
}