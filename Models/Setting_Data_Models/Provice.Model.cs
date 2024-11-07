using AutoMapper;
using CsvHelper.Configuration.Attributes;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models.Setting_Data_Models
{
    // DONE
    public class Province : BaseEntity<string>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
            cfg.CreateMap<District, ProvinceResponse>()
        );

        static readonly IMapper mapper = config.CreateMapper();

        public ProvinceResponse ToResponse()
        {
            var res = mapper.Map<ProvinceResponse>(this);
            return res;
        }

        public string Name { get; set; }

        public ICollection<District>? Districts { get; set; }
    }
}
