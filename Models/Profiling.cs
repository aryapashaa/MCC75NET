using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCC75NET.Models;

[Table("tb_tr_profilings")]
public class Profiling
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Display(Name = "Employee")]
    [Required, Column("employee_id", TypeName = "nchar(5)")]
    public string EmployeeId { get; set; }
    [Display (Name= "Education")]
    [Required, Column("education_id")]
    public int EducationId { get; set; }

    // Relation and Cardinality
    [ForeignKey(nameof(EducationId))]
    public Education? Education { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Employee? Employee { get; set; }
}
