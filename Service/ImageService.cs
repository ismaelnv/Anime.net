using AnimeWeb.Repository.IRepository;
using AnimeWeb.Service.Interface;
using AutoMapper;

namespace AnimeWeb.Service
{
    public class ImageService : IImageService
    {

        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(IImageRepository imageRepository,IMapper mapper)
        {

            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<ImageModel?> createImage(ImageModel imageModel)
        { 

            if (imageModel == null)
            {
                throw new BadHttpRequestException("Invalid image");
            }


            await _imageRepository.CreateAsync(imageModel);
            
            return imageModel;
        }

        public async Task uploadImage(int imageId, IFormFile image)
        {

            if (imageId == 0)
            {

                throw new BadHttpRequestException("Id invalid");
            }

            ImageModel imageModel = await _imageRepository.ObtainAsync(i => i.id == imageId);

            if (imageModel == null)
            {

                throw new("Couldn't find the Image");
            }

            //Obtener la ruta de la carpeta
            string paht = Path.Combine(Directory.GetCurrentDirectory(),"Images");
            //crear la carpeta si  no  existe o no se encuentra
            if(!Directory.Exists(paht))
            {

                Directory.CreateDirectory(paht);
            }

            //obteniendo el nombre del archivo
            FileInfo fileInfo = new FileInfo(image.FileName);
           // string uniqueFileName = file.Name + fileInfo.Extension;
           //Creando un nombre unico para las imagenes con GUILD
            string uniqueFileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
            imageModel.name = uniqueFileName;
            //convinar el nombe de la imagen con la carpeta para crear un nombre unico    
            string fileNameWithPath = Path.Combine(paht,uniqueFileName);

            //agregar la imagen al paquete
            using ( var stream = new FileStream(fileNameWithPath,FileMode.Create))
            {

                image.CopyTo(stream);
            }

            await _imageRepository.EngraveAsync();
        }

        public Task<ImageModel?> getImageId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ImageModel>> getImages()
        {
            IEnumerable<ImageModel> images = await _imageRepository.GetAllAsync();
            return images; 
        }

        public async Task<ImageModel?> removeImage(int id)
        { 

            if (id == 0)
            {
                throw new BadHttpRequestException("Id invalid");
            }

            ImageModel? image = await this.getImageId(id);

            if (image == null)
            {
                return null;
            }

            await _imageRepository.RemoveAsync(image);

            return image;
        }

    }
}        
