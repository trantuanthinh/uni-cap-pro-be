using System.ComponentModel.DataAnnotations;
using AutoMapper;
using uni_cap_pro_be.Core.Data.Base.Entity;
using uni_cap_pro_be.DTO.Response;

namespace uni_cap_pro_be.Models
{
    // DONE
    public class Comment : BaseEntity<Guid>
    {
        static readonly MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Comment, CommentResponse>();
        });

        static readonly IMapper mapper = config.CreateMapper();

        public CommentResponse ToResponse()
        {
            var res = mapper.Map<CommentResponse>(this);
            return res;
        }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        public required string Content { get; set; }
        public required int Rating { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Modified_At { get; set; }

        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}
