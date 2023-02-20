using MCC75NET.Contexts;
using MCC75NET.Models;
using Microsoft.AspNetCore.Mvc;
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
        var accounts = context.Accounts.ToList();
        return View(accounts);
    }
    public IActionResult Details(int id)
    {
        var account = context.Accounts.Find(id);
        return View(account);
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

    public IActionResult Delete(int id)
    {
        var account = context.Accounts.Find(id);
        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
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
}
