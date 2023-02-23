using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MCC75NET.ViewModels;

public class AccountEmployeeVM
{
    [EmailAddress]
    [Display(Name = "E-Mail")]
    public string EmployeeEmail { get; set; }
    public string Password { get; set; }
}
