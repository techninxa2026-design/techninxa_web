using System.ComponentModel.DataAnnotations;

namespace techninxa.Models
{
    /// <summary>
    /// Corporate website contact form. Persisted into the existing ContactMessages
    /// table so no new database schema or migration is needed.
    /// </summary>
    public class CorporateInquiryViewModel
    {
        [Required(ErrorMessage = "Your name is required.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(120)]
        public string? Company { get; set; }

        [Required(ErrorMessage = "A contact email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A phone number is required.")]
        [StringLength(45)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a service division.")]
        [StringLength(80)]
        public string ServiceDivision { get; set; } = string.Empty;

        [StringLength(80)]
        public string InquiryType { get; set; } = "Request a quotation";

        [Required(ErrorMessage = "Please describe your requirement.")]
        [StringLength(1300, MinimumLength = 10,
            ErrorMessage = "Please provide between 10 and 1300 characters.")]
        public string Details { get; set; } = string.Empty;

        // Invisible spam honeypot; legitimate users leave this blank.
        public string? Website { get; set; }
    }
}
