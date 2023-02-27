using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories;
using MCC75NET.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Data;

namespace MCC75NET.Controllers;
public class EducationController : Controller
{
    private readonly EducationRepository educationRepository;
    private readonly UniversityRepository universityRepository;

    public EducationController(EducationRepository educationRepository,
        UniversityRepository universityRepository)
    {
        this.educationRepository = educationRepository;
        this.universityRepository = universityRepository;
    }

    [Authorize]
    public IActionResult Index()
    {
        var educations = educationRepository.GetEducationUniversities();
        return View(educations);
    }

    [Authorize]
    public IActionResult Details(int id)
    {
        return View(educationRepository.GetByIdEducations(id));
        
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.University = universities;
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EducationUniversityVM educations)
    {
        var result = educationRepository.Insert(new Education
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityId = Convert.ToInt32(educations.UniversityName)
        });
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.University = universities;

        return View(educationRepository.GetByIdEducations(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EducationUniversityVM educations)
    {
        var result = educationRepository.Update(new Education
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityId = Convert.ToInt32(educations.UniversityName)
        });
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var educations = educationRepository.GetById(id);
        return View(new EducationUniversityVM
        {
            Id = educations.Id,
            Degree = educations.Degree,
            GPA = educations.GPA,
            Major = educations.Major,
            UniversityName = universityRepository.GetById(educations.UniversityId).Name
        });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = educationRepository.Delete(id);
        if (result == 0)
        {
            // Data Tidak Ditemukan
        }
        else
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete));
    }
}
