using AnimeWeb.Data;
using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            await _animeRepository.Update(anime);
            return anime;
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
 
        //probando metodo que traera animes y sus capitulos 
        public async Task<AnimeModel> getAnimeCapitulos(int id){
            var anime =  await _bd.Anime.Where(A => A.Id  == id).Include(A => A.chapters).FirstAsync();
            return anime;
        }

    }
}