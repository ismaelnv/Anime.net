using AnimeWeb.Models;
using AnimeWeb.Models.Dto;
using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service.Interface;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class StudioService : IStudioService
    {

        private readonly IStudioRepository _studioRepository;
        private readonly IMapper _mapper;

        public StudioService(IStudioRepository studioRepository,IMapper mapper)
        {

            _studioRepository = studioRepository;
            _mapper = mapper;
        } 

        public async Task<StudioModel> createStudio(CreateStudioDto createStudioDto)
        {

            if (createStudio == null)
            {
                throw new BadHttpRequestException("Invalid Studio");
            }

            StudioModel studio = _mapper.Map<StudioModel>(createStudioDto);
            studio.uploadDate = DateTime.Now;
            studio.updateDate = DateTime.Now;

            await _studioRepository.CreateAsync(studio);

            return studio;
        }

        public async Task<StudioModel?> getStudioId(int id)
        {

            if(id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            StudioModel studio = await _studioRepository.ObtainAsync(v => v.id == id);

            if (studio == null)
            {
                return null;
            }

            return studio;
        }

        public async Task<IEnumerable<StudioDto>> getStudios()
        {

            IEnumerable<StudioModel> studiosModel = await _studioRepository.GetAllAsync();
            IEnumerable<StudioDto> studios = _mapper.Map<IEnumerable<StudioDto>>(studiosModel);
            return studios;
        }

        public async Task<StudioModel?> removeStudio(int id)
        {
            
            if (id == 0)
            {
                throw new BadHttpRequestException("Invalid Id");
            }

            StudioModel? studio = await this.getStudioId(id);

            if (studio == null)
            {
                return null;
            }

            await _studioRepository.RemoveAsync(studio);
            
            return studio;
        }

        public async Task<StudioModel?> updateStudio(int id, UpdateStudioDto updateStudioDto)
        {

            if (updateStudioDto.id != id)
            {
                throw new BadHttpRequestException("Id does not match the studio id");
            }

            if (updateStudioDto == null)
            {
                throw new BadHttpRequestException("Invalid studio"); 
            }

            StudioModel studioModel = _mapper.Map<StudioModel>(updateStudioDto);
            StudioModel studio = await _studioRepository.UpdateAsync(studioModel);
            
            return studio;
        }

        public async Task<List<StudioModel>> getStudiosId(List<int> studioIds)
        {

            List<StudioModel> studios = await _studioRepository.GetByIdsAsync(studioIds);
            return studios;
        }

        public async Task<StudioModel?> getStudioAndAnimes(int id)
        {
            if ( id  == 0)
            {

                throw new BadHttpRequestException("Invalid Id");
            }

            StudioModel studio = await _studioRepository.GetStudioAndAnimes(id);

            if (studio == null)
            {

                return null;
            }

            return studio;
        } 
    }
}