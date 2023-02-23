using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MCC75NET.Controllers;
public class AccountController : Controller
{
    private readonly MyContext context;
    private readonly AccountRepository accountRepository;

    public AccountController(MyContext context, AccountRepository accountRepository)
    {
        this.context = context;
        this.accountRepository = accountRepository;
    }

    public IActionResult Index()
    {
        var results = accountRepository.GetAccountEmployees();
        return View(results);
    }
    public IActionResult Details(string id)
    {
        return View(accountRepository.GetByIdEmployee(id));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Account account)
    {
        var result = accountRepository.Insert(account);
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(string id)
    {
        var account = accountRepository.GetById(id);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Account account)
    {
        var result = accountRepository.Update(account);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(string id)
    {
        var account = accountRepository.GetById(id);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string id)
    {
        var result = accountRepository.Delete(id);
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
            accountRepository.Register(registerVM);

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
    public IActionResult Login(LoginVM loginVM)
    {
        var getAccounts = context.Accounts.Join(
        context.Employees,
        a => a.EmployeeNIK,
        e => e.NIK,
        (a, e) => new LoginVM
        {
            Email = e.Email,
            Password = a.Password
        });

        if (getAccounts.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password))
        {
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "E-Mail or Password Not Found!");

        return View();
    }
}