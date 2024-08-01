using System.Data;
using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service.Interface;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class AnimeService : IAnimeService
    {

        private IAnimeRepository _animeRepository;
        private IMapper _mapper;
        private IStudioService _studioService;

        private IGenreService _genreServie;

        private readonly String baseUrl = "http://192.168.1.4:5092/img";

        public AnimeService(IAnimeRepository animeRepository,IMapper mapper,IGenreService genreService,IStudioService studioService)
        {
            
            _studioService = studioService;
            _animeRepository = animeRepository;
            _mapper = mapper;
            _genreServie = genreService;
        }

        public async Task<IEnumerable<AnimeDto>> getAnimes()
        {

            IEnumerable<AnimeModel> animeList = await _animeRepository.GetAnimes();
            IEnumerable<AnimeDto> animes = _mapper.Map<IEnumerable<AnimeDto>>(animeList);
            
            InRouteImages(animes);

            return animes;
        }

        public async Task<AnimeModel?> createAnime(CAnimeDto cAnimeDto)
        {

            if (cAnimeDto.createAnimeDto == null)
            {
                throw new BadHttpRequestException("Invalid anime");
            }

            AnimeModel anime = _mapper.Map<AnimeModel>(cAnimeDto.createAnimeDto);
            anime.uploadDate = DateTime.Now;
            anime.updateDate = DateTime.Now;

            await _animeRepository.CreateAsync(anime);

            if (cAnimeDto.createAnimeDto != null && cAnimeDto.studioIds.Any() && cAnimeDto.genreIds.Any())
            {

                await this.animeRelationshipWithStudios(anime.Id,cAnimeDto.studioIds);
                await this.relateAnimesAndGenres(anime.Id, cAnimeDto.genreIds);
            }
            
            return anime;
        }

        public async Task<AnimeModel?> updateAnime(int id, UpdateAnimeDto updateAnimeDto)
        {

            if (id != updateAnimeDto.Id)
            {
                throw new BadHttpRequestException("Id does not match the anime id");
            }

            AnimeModel anime = _mapper.Map<AnimeModel>(updateAnimeDto);

            AnimeModel animeModel = await _animeRepository.UpdateAsync(anime);

            if (animeModel == null)
            {
                return null;
            }

            return animeModel;
        }

        public async Task<AnimeModel?> getAnimeId(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            AnimeModel anime = await _animeRepository.GetAnimeByIdWithAttributs(id);

            if (anime != null)
            {
                return anime;
            }

            return null;
        }

        public async Task<AnimeModel?> removeAnime(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            AnimeModel? anime = await this.getAnimeId(id);

            if (anime == null)
            {
                return null;
            }

            await _animeRepository.RemoveAsync(anime);

            return anime;
        }

        public async Task<AnimeModel?> getAnimeChapters(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            AnimeModel anime = await _animeRepository.GetanimeChaptersAsync(id);

            if (anime == null)
            {
                return null;
            }

           var chapters = anime.Chapters;

            foreach (ChapterModel chapter in chapters)
            {
                foreach ( ImageModel image in chapter.Images)
                {

                    image.name =  $"{baseUrl}/{image.name}";
                }
            }

            return anime;
        }

        public async Task relateAnimesAndGenres(int id,List<int> genreIds)
        {

           AnimeModel? anime = await this.getAnimeId(id);
            
            if ( anime == null)
            {
                
            }

            List<GenreModel> genres = await _genreServie.getGenresId(genreIds); 

            foreach (GenreModel genre in genres)
            {
                anime.Genres.Add(genre);
            }

            await _animeRepository.EngraveAsync();
        }

        public async Task<AnimeModel?> getAnimeAndGenres(int id)
        {

            if ( id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            AnimeModel anime = await _animeRepository.GetAnimeAndGenres(id);

            if (anime == null)
            {
                return null;
            }

            return anime;
        }

        public async Task<IEnumerable<AnimeDto>> getLatestAnimesAdded()
        {

            IEnumerable<AnimeDto> animes = await this.getAnimes();
            var animesArrangedInDescendingOrder = animes.OrderByDescending(anime => anime.Id);
            
            IEnumerable<AnimeDto> theLastFifty = animesArrangedInDescendingOrder.Take(50).ToList();
            
            return theLastFifty;
        }

        public async Task animeRelationshipWithStudios(int animeId, List<int> studioIds)
        {

            AnimeModel? anime = await this.getAnimeId(animeId);
            
            if ( anime == null)
            {
                //buscar como crear un execion de not foud
                throw new ("The anime you are looking for was not found");
            }

            List<StudioModel> studios = await _studioService.getStudiosId(studioIds); 

            foreach (StudioModel studio in studios)
            {
                anime?.Studios.Add(studio);
            }

            await _animeRepository.EngraveAsync();
        }

        public async Task<AnimeModel?> getAnimeAndStudios(int id)
        {

            if ( id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            AnimeModel anime = await _animeRepository.GetAnimeAndStudios(id);

            if (anime == null)
            {
                return null;
            }

            return anime;
        }

        public async Task<AnimeModel?> getAnimeAndImages(int id)
        {

            if ( id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            AnimeModel anime = await _animeRepository.GetAnimeAndImages(id);

            if (anime == null)
            {
                return null;
            }

            return anime;
        }

        public async Task<IEnumerable<AnimeDto?>> getAnimesByName(string animeName)
        {

            if(animeName == null)
            {
                throw new BadHttpRequestException("Enter a name from an anime");
            }

            List<AnimeModel> animes = await _animeRepository.GetAnimesByName(animeName);
            IEnumerable<AnimeDto> animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);

            if(animesDto == null)
            {
                return null;
            }

            InRouteImages(animesDto);
            return animesDto;
        }

        public void InRouteImages(IEnumerable<AnimeDto?> animes)
        {
            
            foreach (var anime in animes)
            {
        
                foreach (var image in anime.Images ){

                    image.name = $"{baseUrl}/{image.name}";
                } 
                
            }
        }
    }
}




