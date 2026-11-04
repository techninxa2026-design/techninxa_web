using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace techninxa.Controllers;

public class CompanyChatController : Controller
{
	private readonly IWebHostEnvironment _environment;

	public CompanyChatController(IWebHostEnvironment environment)
	{
		_environment = environment;
	}

	[HttpPost]
	[RequestSizeLimit(10485760L)]
	public async Task<IActionResult> UploadFile(IFormFile file)
	{
		if (HttpContext.Session.GetString("CompanyChatLoggedIn") != "true")
		{
			return Unauthorized(new
			{
				success = false,
				message = "You are not logged in."
			});
		}
		if (file == null || file.Length == 0L)
		{
			return BadRequest(new
			{
				success = false,
				message = "Please select a file."
			});
		}
		if (file.Length > 10485760)
		{
			return BadRequest(new
			{
				success = false,
				message = "File size cannot exceed 10 MB."
			});
		}
		string[] array = new string[11]
		{
			".jpg", ".jpeg", ".png", ".gif", ".webp", ".pdf", ".doc", ".docx", ".xls", ".xlsx",
			".zip"
		};
		string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
		if (!array.Contains(extension))
		{
			return BadRequest(new
			{
				success = false,
				message = "This file type is not allowed."
			});
		}
		string text = Path.Combine(_environment.WebRootPath, "uploads", "chat");
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string safeFileName = $"{Guid.NewGuid():N}{extension}";
		string path = Path.Combine(text, safeFileName);
		await using (FileStream stream = new FileStream(path, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}
		string fileName = Path.GetFileName(file.FileName);
		string fileUrl = "/uploads/chat/" + safeFileName;
		int num;
		switch (extension)
		{
		default:
			num = ((extension == ".webp") ? 1 : 0);
			break;
		case ".jpg":
		case ".jpeg":
		case ".png":
		case ".gif":
			num = 1;
			break;
		}
		bool isImage = (byte)num != 0;
		return Ok(new
		{
			success = true,
			fileName = fileName,
			fileUrl = fileUrl,
			isImage = isImage,
			fileSize = file.Length
		});
	}
}
