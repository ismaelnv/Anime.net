using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service.Interface;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class GenreService : IGenreService
    {

        private readonly IGenreRepository _categorieRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository categorieRepository,IMapper mapper)
        {

            _categorieRepository = categorieRepository;
            _mapper = mapper;
        }  

        public async Task<GenreModel> createCategorie(CreateGenreDto createCategorieDto)
        {

            if (createCategorieDto == null)
            {
                throw new BadHttpRequestException("Invalid categorie");
            }

            GenreModel categorie = _mapper.Map<GenreModel>(createCategorieDto);
            categorie.uploadDate = DateTime.Now;
            categorie.updateDate = DateTime.Now;

            await _categorieRepository.CreateAsync(categorie);

            return categorie;
        }

        public async Task<GenreModel?> getCategorieId(int id)
        {

            if(id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            GenreModel categorie = await _categorieRepository.ObtainAsync(v => v.id == id);

            if (categorie == null)
            {
                return null;
            }

            return categorie;
        }

        public async Task<IEnumerable<GenreDto>> getCategories()
        {

            IEnumerable<GenreModel> categorieModels = await _categorieRepository.GetAllAsync();
            IEnumerable<GenreDto> categories = _mapper.Map<IEnumerable<GenreDto>>(categorieModels);
            return categories;
        }

        public async Task<GenreModel?> removeCategorie(int id)
        {
            
            if (id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            GenreModel? categorie = await this.getCategorieId(id);

            if (categorie == null)
            {
                return null;
            }

            await _categorieRepository.RemoveAsync(categorie);
            
            return categorie;
        }

        public async Task<GenreModel?> updateCategorie(int id, updateGenreDto updateCategorieDto)
        {

            if (updateCategorieDto.id != id)
            {
                throw new BadHttpRequestException("Id does not match the caregorie id");
            }

            if (updateCategorieDto == null)
            {
                throw new BadHttpRequestException("Invalid categorie"); 
            }

            GenreModel categorieModel = _mapper.Map<GenreModel>(updateCategorieDto);
            GenreModel categorie = await _categorieRepository.UpdateAsync(categorieModel);
            
            return categorie;
        }

        public async Task<GenreModel?> getGenreAnimes(int id)
        {

            if (id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            GenreModel genre = await _categorieRepository.getGenreAnimesAsync(id);

            if (genre == null)
            {
                return null;
            }

            return genre;
        }
    }
}