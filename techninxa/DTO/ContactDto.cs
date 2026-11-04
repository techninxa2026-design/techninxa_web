using System.ComponentModel.DataAnnotations;
using Techninxa.Models;

namespace Techninxa.DTO;

public class ContactDto
{
	[Required]
	[MaxLength(100)]
	public string Name { get; set; }

	[Required]
	[EmailAddress]
	[MaxLength(150)]
	public string Email { get; set; }

	[Required]
	[EnumDataType(typeof(ProjectType))]
	public ProjectType ProjectType { get; set; }

	[Required]
	[MaxLength(2000)]
	public string Message { get; set; }
}
