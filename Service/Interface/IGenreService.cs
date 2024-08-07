
using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Service.Interface
{
    public interface IGenreService
    {

        Task<IEnumerable<GenreDto>> getGenres();
        Task<GenreModel> createGenre(CreateGenreDto createCategorieDto);
        Task<GenreModel?> updateGenre(int id, updateGenreDto updateCategorieDto);
        Task<GenreModel?> getGenreId(int id);
        Task<GenreModel?> removeGenre(int id);
        Task<GenreModel?> getGenreAnimes(int id);
        Task<IEnumerable<AnimeDto?>> getAnimesByGenre(string nombreGenero);
        Task<List<GenreModel>> getGenresId(List<int> genreIds);
    }
}