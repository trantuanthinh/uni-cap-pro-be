using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data.Base.Entity
{
	public class BaseEntity<IdType>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public required IdType Id { get; set; }
	}
}
