using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Controllers;
public class ProfilingController : Controller
{
    private readonly MyContext context;

    public ProfilingController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        //var profilings = context.Profilings.ToList();
        var results = context.Profilings.Join(
            context.Educations,
            p => p.EducationId,
            e => e.Id,
            (p, e) => new Profiling
            {
                EducationId = e.Id,
            });
        return View(results);
    }
    public IActionResult Details(int id)
    {
        var profiling = context.Profilings.Find(id);
        return View(profiling);
    }

    public IActionResult Create()
    {
        var employees = context.Employees.ToList()
            .Select(em => new SelectListItem
            {
                Value = em.NIK,
                Text = em.FirstName + " " + em.LastName
            });
        ViewBag.Employee = employees;

        var educations = context.Educations.ToList()
            .Select(ed => new SelectListItem
            {
                Value = ed.Id.ToString(),
                Text = ed.Major
            });
        ViewBag.Education = educations;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Profiling profiling)
    {
        context.Add(profiling);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var profiling = context.Profilings.Find(id);
        return View(profiling);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Profiling profiling)
    {
        context.Entry(profiling).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var profiling = context.Profilings.Find(id);
        return View(profiling);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var profiling = context.Profilings.Find(id);
        context.Remove(profiling);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
