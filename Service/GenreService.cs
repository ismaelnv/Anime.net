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

            IEnumerable<GenreModel> genresModel = await _genreRepository.GetAllAsync();
            IEnumerable<GenreDto> genres = _mapper.Map<IEnumerable<GenreDto>>(genresModel);
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
    }
}