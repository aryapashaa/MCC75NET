using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MCC75NET.Models;

[Table("tb_m_employees")]
public class Employee
{
    [MaxLength(5), MinLength(5, ErrorMessage = "Harus 5 karakter, contoh: 12345")]
    [Key, Column("nik", TypeName = "nchar(5)")]
    public string NIK { get; set; }
    [Display(Name = "First Name")]
    [Required, Column("first_name"), MaxLength(50)]
    public string FirstName { get; set; }
    [Display(Name = "Last Name")]
    [Column("last_name"), MaxLength(50)]
    public string? LastName { get; set; }
    [Display(Name = "Birth Date")]
    [Required, Column("birth_date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }
    [Required, Column("gender")]
    public GenderEnum Gender { get; set; }
    [Display(Name = "Hiring Date")]
    [Required, Column("hiring_date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime HiringDate { get; set; } = DateTime.Now;
    [Display(Name = "E-Mail")]
    [Required, Column("email"), MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    [Display(Name = "Phone Number")]
    [Column("phone_number"), MaxLength(20)]
    public string? PhoneNumber { get; set; }

    // Cardinality
    public ICollection<Profiling>? Profilings { get; set; }
    public Account? Account { get; set; }
    public enum GenderEnum
    {
        Male,
        Female
    }
}
