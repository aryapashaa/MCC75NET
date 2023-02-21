using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Controllers;
public class AccountController : Controller
{
    private readonly MyContext context;

    public AccountController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var results = context.Accounts.Join(
            context.Employees,
            a => a.EmployeeNIK,
            e => e.NIK,
            (a, e) => new AccountVM
            {
                Password = a.Password,
                EmployeeEmail = e.Email,
            });
        return View(results);
    }
    public IActionResult Details(string id)
    {
        var accounts = context.Accounts.Find(id);
        return View(new AccountVM
        {
            Password = accounts.Password,
            EmployeeEmail = context.Employees.Find(accounts.EmployeeNIK).Email
        });
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Account account)
    {
        context.Add(account);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var account = context.Accounts.Find(id);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Account account)
    {
        context.Entry(account).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(string id)
    {
        var account = context.Accounts.Find(id);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string id)
    {
        var account = context.Accounts.Find(id);
        context.Remove(account);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // GET : Account/Register
    public IActionResult Register()
    {
        var gender = new List<SelectListItem>{
            new SelectListItem
            {
                Value = "0",
                Text = "Male"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Female"
            }
        };
        ViewBag.Gender = gender;
        return View();
    }
    // POST : Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVM registerVM)
    {
        if (ModelState.IsValid)
        {
            // Bikin kondisi untuk mengecek apakah data university sudah ada
            //if (context.Universities.Any())
            //{
            //    return View();
            //}
            
            University university = new University
            {
                Name = registerVM.UniversityName
            };
            context.Universities.Add(university);
            context.SaveChanges();

            Education education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = university.Id
            };
            context.Educations.Add(education);
            context.SaveChanges();

            Employee employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = (Employee.GenderEnum)registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            Account account = new Account
            {
                EmployeeNIK = registerVM.NIK,
                Password = registerVM.Password
            };
            context.Accounts.Add(account);
            context.SaveChanges();

            AccountRole accountRole = new AccountRole
            {
                AccountNIK = registerVM.NIK,
                RoleId = 3
            };
            context.AccountRoles.Add(accountRole);
            context.SaveChanges();

            Profiling profiling = new Profiling
            {
                EmployeeId = registerVM.NIK,
                EducationId = education.Id
            };
            context.Profilings.Add(profiling);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    //Login
    //GET : Account/Login
    public IActionResult Login()
    {
        return View();
    }

    //POST : Account/Login
    // Parameter LoginVM {Email, Password}
    // Validasi Email exist?, Password equal?
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async IActionResult Login(LoginVM loginVM)
    {
        if (ModelState.IsValid)
        {
            Employee employee = new Employee();
            //{
            //    Email = loginVM.Email
            //};
            Account account = new Account();
            //{
            //    Password = loginVM.Password
            //};
            if (loginVM.Email == employee.Email)
            {
                if (loginVM.Password == account.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        return View();
    }
}

