using AnimeWeb.Models;
using AnimeWeb.Models.Dto;

namespace AnimeWeb.Service
{
    public interface IAnimeService
    {
        Task<IEnumerable<AnimeDto>> getAnimes();
        Task<AnimeModel?> createAnime(CAnimeDto cAnimeDto);
        Task<AnimeModel?> updateAnime(int id, UpdateAnimeDto updateAnimeDto);
        Task<AnimeModel?> getAnimeId(int id);
        Task<AnimeModel?> removeAnime(int id);
        Task<AnimeModel?> getAnimeChapters(int id);
        Task relateAnimesAndGenres(int id,List<int> genreIds);
        Task<AnimeModel?> getAnimeAndGenres(int id);
        Task<IEnumerable<AnimeDto>> getLatestAnimesAdded();
        Task animeRelationshipWithStudios(int animeId, List<int> studioIds);
        Task<AnimeModel?> getAnimeAndStudios(int id);
    }
}