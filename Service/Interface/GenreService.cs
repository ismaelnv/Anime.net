
using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Service.Interface
{
    public interface IGenreService
    {

        Task<IEnumerable<GenreDto>> getCategories();
        Task<GenreModel> createCategorie(CreateGenreDto createCategorieDto);
        Task<GenreModel?> updateCategorie(int id, updateGenreDto updateCategorieDto);
        Task<GenreModel?> getCategorieId(int id);
        Task<GenreModel?> removeCategorie(int id);
        Task<GenreModel?> getGenreAnimes(int id);
    }
}