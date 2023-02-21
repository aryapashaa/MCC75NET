using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCC75NET.ViewModels;

public class AccountVM
{
    [Display(Name = "E-Mail")]
    public string EmployeeEmail { get; set; }
    public string Password { get; set; }
}
