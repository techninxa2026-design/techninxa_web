using System.ComponentModel.DataAnnotations;

namespace techninxa.Models;

public class AdminUser
{
	public int Id { get; set; }

	[Required]
	[EmailAddress]
	[MaxLength(150)]
	public string Username { get; set; }

	[Required]
	public string Password { get; set; }
}
