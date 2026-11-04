using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Techninxa.Models;

public class ProjectMedia
{
	public int Id { get; set; }

	[Required]
	public int ProjectId { get; set; }

	[ForeignKey("ProjectId")]
	public Project Project { get; set; }

	[Required]
	[MaxLength(20)]
	public string MediaType { get; set; } = string.Empty;

	[Required]
	[MaxLength(500)]
	public string FilePath { get; set; } = string.Empty;

	[MaxLength(255)]
	public string? OriginalFileName { get; set; }

	[MaxLength(100)]
	public string? ContentType { get; set; }

	public long FileSize { get; set; }

	public int DisplayOrder { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
