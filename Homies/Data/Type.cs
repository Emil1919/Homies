using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Homies.Data
{
	public class Type
	{
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string Name { get; set; } = null!;
        public HashSet<Event> Events { get; set; } = new HashSet<Event>();

      
    }
}
