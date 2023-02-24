using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace MCC75NET.Controllers;
public class EmployeeController : Controller
{
    private readonly EmployeeRepository employeeRepository;

    public EmployeeController(EmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employees = employeeRepository.GetAll();
        return View(employees);
    }
    public IActionResult Details(string id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employee = employeeRepository.GetById(id);
        return View(employee);
    }

    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee employee)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Insert(employee);
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(string id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employee = employeeRepository.GetById(id);
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Employee employee)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Update(employee);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(string id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employee = employeeRepository.GetById(id);
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string nik)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Delete(nik);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete));
    }
}