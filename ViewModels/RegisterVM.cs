using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCC75NET.ViewModels;

public class RegisterVM
{
    [MaxLength(5), MinLength(5, ErrorMessage = "Harus 5 karakter, contoh: 12345")]
    [Key, Column("nik", TypeName = "nchar(5)")]
    public string NIK { get; set; }
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }
    [Display(Name = "Birth Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }
    public GenderEnum Gender { get; set; }
    [Display(Name = "Hiring Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime HiringDate { get; set; } = DateTime.Now;
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    [Range(0, 4, ErrorMessage = "Inputan Harus Lebih dari {1} dan Kurang dari {2}")]
    public float GPA { get; set; }
    [Display(Name = "University Name")]
    public string UniversityName { get; set; }
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Password { get; set; }
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password),ErrorMessage ="Not Match")]
    public string ConfirmPassword { get; set; }

    public enum GenderEnum
    {
        Male,
        Female
    }
}