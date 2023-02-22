using System.ComponentModel.DataAnnotations;

namespace MCC75NET.ViewModels;

public class EducationUniversityVM
{
    public int Id { get; set; }
    public string Major { get; set; }
    [MaxLength(2), MinLength(2, ErrorMessage = "Contoh inputan : S1/D3")]
    [Required(ErrorMessage = "Tidak Boleh Kosong, Contoh inputan : S1/D3")]
    public string Degree { get; set; }
    [Range(0, 4, ErrorMessage = "Inputan harus lebih dari {1} dan kurang dari {2}")]
    public float GPA { get; set; }
    [Display(Name = "University Name")]
    public string UniversityName { get; set; }
}
