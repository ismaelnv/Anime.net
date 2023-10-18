using AnimeWeb.Models;
using AnimeWeb.Repository.IRepository;

namespace AnimeWeb.Service
{
    public class CapituloService
    {
        private readonly ICapituloRepository _capituloRepository;

        public CapituloService(ICapituloRepository capituloRepository)
        {
            _capituloRepository = capituloRepository;
        }

        public async Task<IEnumerable<CapituloModel>> getCapitulos()
        {
            return await _capituloRepository.GetAll();
        }

        public async Task<CapituloModel> getCapituloId(int id)
        {
            if(id == 0){
                return null;
            }

            var capitulo = await _capituloRepository.Obtain(C => C.id == id);
            return capitulo;
        }

        public async Task<CapituloModel> createCapitulo(CapituloModel capituloModel)
        {
            if(capituloModel == null)
            {
                return null;
            }

            await _capituloRepository.Create(capituloModel);
            return capituloModel;
        } 

        public async Task<CapituloModel> removeCapitulo(int id)
        {
            var capitulo = await this.getCapituloId(id);
            await _capituloRepository.Remove(capitulo);
            return capitulo;
        }

        public async Task<CapituloModel> updateCapitulo(int id, CapituloModel capituloModel)
        {
            if (capituloModel.id != id || capituloModel == null)
            {
                return null;
            }

            var capitulo = await _capituloRepository.Update(capituloModel);
            return capitulo;
        }

    }
}