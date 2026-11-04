using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Techninxa.Models;

public class ProjectCreateViewModel
{
	[Required]
	[MaxLength(100)]
	public string Title { get; set; } = string.Empty;

	[Required]
	[MaxLength(50)]
	public string Category { get; set; } = string.Empty;

	[MaxLength(150)]
	public string? Client { get; set; }

	public int? Year { get; set; }

	[MaxLength(500)]
	public string? ProjectUrl { get; set; }

	[MaxLength(300)]
	public string? ShortDescription { get; set; }

	public string? Overview { get; set; }

	[Required]
	public IFormFile? HeroImage { get; set; }

	public List<IFormFile> Images { get; set; } = new List<IFormFile>();

	public List<IFormFile> Videos { get; set; } = new List<IFormFile>();

	public List<string> Technologies { get; set; } = new List<string>();

	public List<string> FeatureTitles { get; set; } = new List<string>();

	public List<string> FeatureDescriptions { get; set; } = new List<string>();

	public int? NextProject { get; set; }

	[MaxLength(200)]
	public string Slug { get; set; } = string.Empty;

	public string Status { get; set; } = "draft";
}
