using System.ComponentModel.DataAnnotations;

namespace techninxa.Models;

public class HolidaySetting
{
	public int Id { get; set; }

	[Required]
	public Weekday Day { get; set; }

	public bool IsHoliday { get; set; }
}
