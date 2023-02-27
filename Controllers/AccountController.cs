using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MCC75NET.Controllers;
public class AccountController : Controller
{
    private readonly AccountRepository accountRepository;
    private readonly EmployeeRepository employeeRepository;
    private readonly IConfiguration configuration;

    public AccountController(AccountRepository accountRepository, EmployeeRepository employeeRepository, IConfiguration configuration)
    {
        this.accountRepository = accountRepository;
        this.employeeRepository = employeeRepository;
        this.configuration = configuration;
    }

    public IActionResult Index()
    {
        var results = accountRepository.GetAccountEmployees();
        return View(results);
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
        if (accountRepository.Login(loginVM))
        {
            var userdata = accountRepository.GetUserdata(loginVM.Email);
            var roles = accountRepository.GetRolesByNIK(loginVM.Email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.FullName)
            };

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signIn
                );

            var generateToken = new JwtSecurityTokenHandler().WriteToken(token);

            HttpContext.Session.SetString("jwtoken", generateToken);

            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "E-Mail or Password Not Found!");

        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index), "Home");
    }
}