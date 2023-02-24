using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Controllers;
public class RoleController : Controller
{
    private readonly RoleRepository roleRepository;

    public RoleController(RoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
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
        var roles = roleRepository.GetAll();
        return View(roles);
    }
    public IActionResult Details(int id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var role = roleRepository.GetById(id);
        return View(role);
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
    public IActionResult Create(Role role)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = roleRepository.Insert(role);
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var role = roleRepository.GetById(id);
        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Role role)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = roleRepository.Update(role);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var role = roleRepository.GetById(id);
        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = roleRepository.Delete(id);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete));
    }
}
