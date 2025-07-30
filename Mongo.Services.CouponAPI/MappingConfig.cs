using AutoMapper;

namespace Mongo.Services.CouponAPI
{
    public class MappingConfig: Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappfingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Models.Coupon, Models.Dto.CouponDto>().ReverseMap();
            });

            return mappfingConfig;
        }

    }
}
