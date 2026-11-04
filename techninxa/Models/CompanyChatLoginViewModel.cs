using System.ComponentModel.DataAnnotations;

namespace techninxa.Models
{
	public class CompanyChatLoginViewModel
	{
		[Required]
		public string Username { get; set; } = "";

		[Required]
		public string Password { get; set; } = "";
	}
}
namespace Techninxa.Models
{
	public class CompanyChatLoginViewModel
	{
		[Required]
		public string Username { get; set; } = "";

		[Required]
		public string Password { get; set; } = "";

		[Required]
		public string DisplayName { get; set; } = "";
	}
}
