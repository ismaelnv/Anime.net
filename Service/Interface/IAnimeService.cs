using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Service
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeDto>> getAnimes();
        Task<AnimeModel> createAnime(CreateAnimeDto createAnimeDto);
        Task<AnimeModel?> updateAnime(int id, UpdateAnimeDto updateAnimeDto);
        Task<AnimeModel?> getAnimeId(int id);
        Task<AnimeModel?> removeAnime(int id);
        Task<AnimeModel?> getAnimeChapters(int id);
    }
}