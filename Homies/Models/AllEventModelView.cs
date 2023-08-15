using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
	public class AllEventModelView
	{
	
		public int Id { get; set; }
		[Required]
		[MinLength(5)]
		[MaxLength(20)]
		public string Name { get; set; } = null!;
		[Required]
		[MinLength(15)]
		[MaxLength(150)]
		public string Description { get; set; } = null!;
		[Required]
		public string OrganiserId { get; set; } = null!;
		[Required]
		public DateTime CreatedOn { get; set; }
		[Required]
		public DateTime Start { get; set; }
		[Required]
		public DateTime End { get; set; }

		public string Type { get; set; } = null!;

		public string Organiser { get; set; } = null!;
		
		
	}
}
