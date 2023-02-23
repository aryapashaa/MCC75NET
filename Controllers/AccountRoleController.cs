using MCC75NET.Contexts;
using MCC75NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCC75NET.Controllers;
public class AccountRoleController : Controller
{
    private readonly MyContext context;

    public AccountRoleController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var account_roles = context.AccountRoles.ToList();
        return View(account_roles);
    }
    public IActionResult Details(int id)
    {
        var account_role = context.AccountRoles.Find(id);
        return View(account_role);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AccountRole account_role)
    {
        context.Add(account_role);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var account_role = context.AccountRoles.Find(id);
        return View(account_role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AccountRole account_role)
    {
        context.Entry(account_role).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var account_role = context.AccountRoles.Find(id);
        return View(account_role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var account_role = context.AccountRoles.Find(id);
        context.Remove(account_role);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Delete));
    }
}
