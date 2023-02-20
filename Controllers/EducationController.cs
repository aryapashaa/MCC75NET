using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Controllers;
public class EducationController : Controller
{
    private readonly MyContext context;

    public EducationController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var results = context.Educations.Join(
            context.Universities,
            e => e.UniversityId,
            u => u.Id,
            (e,u) => new EducationVM
            {
                Id = e.Id,
                Degree = e.Degree,
                GPA = e.GPA,
                Major = e.Major,
                UniversityName = u.Name
            });

        return View(results);
    }
    public IActionResult Details(int id)
    {
        var educations = context.Educations.Find(id);
        return View(new EducationVM
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityName = context.Universities.Find(educations.UniversityId).Name
        });
    }

    public IActionResult Create()
    {
        var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.University = universities;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EducationVM educations)
    {
        context.Add(new Education
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityId = Convert.ToInt32(educations.UniversityName)
        });
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var universities = context.Universities.ToList()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.University = universities;

        var educations = context.Educations.Find(id);
        return View(new EducationVM
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityName = context.Universities.Find(educations.UniversityId).Name
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EducationVM educations)
    {
        context.Entry(new Education
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityId = Convert.ToInt32(educations.UniversityName)
        }).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var educations = context.Educations.Find(id);
        return View(new EducationVM
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityName = context.Universities.Find(educations.UniversityId).Name
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var educations = context.Educations.Find(id);
        context.Remove(educations);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
