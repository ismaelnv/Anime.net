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

        private IGenreService _categorieServie;

        public AnimeService(IAnimeRepository animeRepository,IMapper mapper,IGenreService categorieService)
        {
            
            _animeRepository = animeRepository;
            _mapper = mapper;
            _categorieServie = categorieService;
        }

        public async Task<IEnumerable<AnimeDto>> getAnimes()
        {

            IEnumerable<AnimeModel> animeList = await _animeRepository.GetAllAsync();
            IEnumerable<AnimeDto> animes = _mapper.Map<IEnumerable<AnimeDto>>(animeList);
            return animes;
        }

        public async Task<AnimeModel?> createAnime(CreateAnimeDto createAnimeDto)
        {

            if (createAnimeDto == null)
            {
                throw new BadHttpRequestException("Invalid anime");
            }

            //CategorieModel? categorieModel = await _categorieServie.getCategorieId(createAnimeDto.idCategorie);

            // if ( categorieModel == null)
            // {
            //     return null;
            // }

            AnimeModel anime = _mapper.Map<AnimeModel>(createAnimeDto);
            anime.uploadDate = DateTime.Now;
            anime.updateDate = DateTime.Now;
            anime.Genres = createAnimeDto.Genres;

            await _animeRepository.CreateAsync(anime);

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

            AnimeModel anime = await _animeRepository.ObtainAsync(A => A.Id == id);

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

            AnimeModel anime = await _animeRepository.getanimeChaptersAsync(id);

            if (anime == null)
            {
                return null;
            }

            return anime;
        }
    }
}



