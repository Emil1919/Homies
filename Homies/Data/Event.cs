
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Homies.Data
{
	public class Event
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[MinLength(5)]
		[MaxLength(20)]
		public string Name { get; set; }	= null!;
		[Required]
		[MinLength(15)]
		[MaxLength(150)]
		public string Description { get; set; } = null!;
		[Required]
		public string OrganiserId { get; set; } =null!;
		public IdentityUser Organiser { get; set; } = null!;
		[Required]
		public DateTime CreatedOn { get; set; } 
		[Required]
		public DateTime Start { get; set; }
		[Required]
		public DateTime End { get; set; }

		[ForeignKey(nameof(Type))]
		public int TypeId { get; set; }
		[Required]
		public Type Type { get; set; } = null!;
        public HashSet <EventParticipant> EventsParticipants { get; set; } = new HashSet<EventParticipant>();


	}
}
