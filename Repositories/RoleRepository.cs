using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MCC75NET.Repositories;

public class RoleRepository : IRepository<int, Role>
{
    private readonly MyContext context;

    public RoleRepository(MyContext context)
    {
        this.context = context;
    }
    public int Delete(int key)
    {
        int result = 0;
        var role = GetById(key);

        if (role == null)
        {
            return result;
        }
        context.Remove(role);
        result = context.SaveChanges();

        return result;
    }

    public List<Role> GetAll()
    {
        return context.Roles.ToList() ?? null;
    }

    public Role GetById(int key)
    {
        return context.Roles.Find(key) ?? null;
    }

    public int Insert(Role entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();

        return result;
    }

    public int Update(Role entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }
}
