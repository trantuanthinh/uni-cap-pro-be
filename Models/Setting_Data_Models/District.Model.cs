using AutoMapper;
using CsvHelper.Configuration.Attributes;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models.Setting_Data_Models
{
    // DONE
    public class District : BaseEntity<string>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<District, DistrictResponse>()
        );

        static readonly IMapper mapper = config.CreateMapper();

        public DistrictResponse ToResponse()
        {
            var res = mapper.Map<DistrictResponse>(this);
            return res;
        }

        public string Name { get; set; }
        public string ProvinceId { get; set; }

        public Province? Province { get; set; }
        public ICollection<Ward>? Wards { get; set; }
    }
}
