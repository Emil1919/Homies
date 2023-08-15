using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
	public class FormAddEventViewModel
	{
		[Required]
		[MinLength(5)]
		[MaxLength(20)]
		public string Name { get; set; } = null!;
		[Required]
		[MinLength(15)]
		[MaxLength(150)]
		public string Description { get; set; } = null!;
		
		[Required]
		public DateTime CreatedOn { get; set; }
		[Required]
		public DateTime Start { get; set; }
		[Required]
		public DateTime End { get; set; }

		public int TypeId { get; set; }

		public List<TypeOfEventViewModel> Types { get; set; } =  new List<TypeOfEventViewModel>();

		public int? Id { get; set; }

		
	}
}
