using System.ComponentModel.DataAnnotations;

namespace WebdevProjectStarterTemplate.Models
{
	public class User
	{
		public int OberId { get; set; }
		[Required, MinLength(2), MaxLength(128)]
		public string? Name { get; set; }
		[Required, MinLength(2), MaxLength(128)]
		public string? Email { get; set; }
		[Required, MinLength(2), MaxLength(128)]
		public string? Password { get; set; }
		[Required, MinLength(2), MaxLength(128)]
		public string? Password2 { get; set; }
	}
}
