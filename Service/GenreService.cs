using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service.Interface;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class GenreService : IGenreService
    {

        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly String baseUrl = "http://192.168.1.4:5092/img";

        public GenreService(IGenreRepository genreRepository,IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }  

        public async Task<GenreModel> createGenre(CreateGenreDto createGenreDto)
        {

            if (createGenreDto == null)
            {
                throw new BadHttpRequestException("Invalid genre");
            }

            GenreModel genre = _mapper.Map<GenreModel>(createGenreDto);
            genre.uploadDate = DateTime.Now;
            genre.updateDate = DateTime.Now;

            await _genreRepository.CreateAsync(genre);

            return genre;
        }

        public async Task<GenreModel?> getGenreId(int id)
        {

            if(id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            GenreModel genre = await _genreRepository.ObtainAsync(v => v.id == id);

            if (genre == null)
            {
                return null;
            }

            return genre;
        }

        public async Task<IEnumerable<GenreDto>> getGenres()
        {

            List<GenreModel> genresModel = await _genreRepository.GetGenres();
            IEnumerable<GenreDto> genres = _mapper.Map<List<GenreDto>>(genresModel);
            return genres;
        }

        public async Task<GenreModel?> removeGenre(int id)
        {
            
            if (id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            GenreModel? genre = await this.getGenreId(id);

            if (genre == null)
            {
                return null;
            }

            await _genreRepository.RemoveAsync(genre);
            
            return genre;
        }

        public async Task<GenreModel?> updateGenre(int id, updateGenreDto updateGenreDto)
        {

            if (updateGenreDto.id != id)
            {
                throw new BadHttpRequestException("Id does not match the genre id");
            }

            if (updateGenreDto == null)
            {
                throw new BadHttpRequestException("Invalid genre"); 
            }

            GenreModel genreModel = _mapper.Map<GenreModel>(updateGenreDto);
            GenreModel genre = await _genreRepository.UpdateAsync(genreModel);
            
            return genre;
        }

        public async Task<GenreModel?> getGenreAnimes(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            GenreModel genre = await _genreRepository.getGenreAnimesAsync(id);

            if (genre == null)
            {
                return null;
            }

            return genre;
        }

        public async Task<IEnumerable<AnimeDto?>> getAnimesByGenre(string nameGenre)
        {

            if (nameGenre == "")
            {
                throw new BadHttpRequestException("Invalid gender name");
            }

            List<AnimeModel> animes = await _genreRepository.GetAnimesByGenreAsync(nameGenre);
            IEnumerable<AnimeDto>animesDto = _mapper.Map<IEnumerable<AnimeDto>>(animes);
            
            foreach (var anime  in animesDto)
            {
                foreach (var image in anime.Images)
                {
                    
                    image.name = $"{baseUrl}/{image.name}";
                }    
            }
            
            return animesDto;
        }

        public async Task<List<GenreModel>> getGenresId(List<int> genreIds)
        {

            List<GenreModel> genres = await _genreRepository.GetByIdsAsync(genreIds);
            return genres;
        }
    }
}