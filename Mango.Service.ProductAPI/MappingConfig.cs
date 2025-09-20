using AutoMapper;
using Mango.Service.ProductAPI.Models;
using Mango.Service.ProductAPI.Models.Dto;

namespace Mongo.Services.ProductAPI
{
    public class MappingConfig : Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappfingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductModel, ProductDto>().ReverseMap();
            });

            return mappfingConfig;
        }

    }
}
