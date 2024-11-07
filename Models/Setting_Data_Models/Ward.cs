using AutoMapper;
using CsvHelper.Configuration.Attributes;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models.Setting_Data_Models
{
    // DONE
    public class Ward : BaseEntity<string>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Ward, WardResponse>()
        );

        static readonly IMapper mapper = config.CreateMapper();

        public WardResponse ToResponse()
        {
            var res = mapper.Map<WardResponse>(this);
            return res;
        }

        public string Name { get; set; }
        public string DistrictId { get; set; }

        public District? District { get; set; }
    }
}
