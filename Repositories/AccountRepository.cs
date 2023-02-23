using MCC75NET.Contexts;
using MCC75NET.Models;
using MCC75NET.Repositories.Interface;
using MCC75NET.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace MCC75NET.Repositories;

public class AccountRepository : IRepository<string, Account>
{
    private readonly MyContext context;
    private readonly UniversityRepository universityRepository;
    private readonly EducationRepository educationRepository;
    private readonly EmployeeRepository employeeRepository;

    public AccountRepository(MyContext context, UniversityRepository universityRepository, 
        EducationRepository educationRepository, EmployeeRepository employeeRepository)
    {
        this.context = context;
        this.universityRepository = universityRepository;
        this.educationRepository = educationRepository;
        this.employeeRepository = employeeRepository;
    }
    public int Delete(string nik)
    {
        int result = 0;
        var account = GetById;

        if (account == null)
        {
            return result;
        }

        context.Remove(account);
        result = context.SaveChanges();

        return result;
    }

    public List<Account> GetAll()
    {
        return context.Accounts.ToList() ?? null;
    }
    public Account GetById(string key)
    {
        return context.Accounts.Find(key) ?? null;
    }
    public int Insert(Account entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();

        return result;
    }

    public int Update(Account entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }

    public List<AccountEmployeeVM> GetAccountEmployees()
    {
        var results = (context.Accounts.Join(
            context.Employees,
            a => a.EmployeeNIK,
            e => e.NIK,
            (a, e) => new AccountEmployeeVM
            {
                Password = a.Password,
                EmployeeEmail = e.Email,
            })).ToList();

        return results;
    }
    public AccountEmployeeVM GetByIdAccount(string key)
    {
        var accounts = GetById(key);
        var result = new AccountEmployeeVM
        {
            EmployeeEmail = employeeRepository.GetById(accounts.EmployeeNIK).Email,
            Password = accounts.Password,
        };

        return result;
    }

    public int Register(RegisterVM entity)
    {
        int result = 0;
        University university = new University
        {
            Name = entity.UniversityName
        };
        // Bikin kondisi untuk mengecek apakah data university sudah ada
        if (context.Universities.Any(u => u.Name == entity.UniversityName))
        {
            university.Id = context.Universities.FirstOrDefault(
                u => u.Name == entity.UniversityName).Id;
        }
        else
        {
            context.Universities.Add(university);
            context.SaveChanges();
        }

        Education education = new Education
        {
            Major = entity.Major,
            Degree = entity.Degree,
            GPA = entity.GPA,
            UniversityId = university.Id
        };
        context.Educations.Add(education);
        context.SaveChanges();

        Employee employee = new Employee
        {
            NIK = entity.NIK,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            Gender = (Employee.GenderEnum)entity.Gender,
            HiringDate = entity.HiringDate,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber
        };
        context.Employees.Add(employee);
        context.SaveChanges();

        Account account = new Account
        {
            EmployeeNIK = entity.NIK,
            Password = entity.Password
        };
        context.Accounts.Add(account);
        context.SaveChanges();

        AccountRole accountRole = new AccountRole
        {
            AccountNIK = entity.NIK,
            RoleId = 3
        };
        context.AccountRoles.Add(accountRole);
        context.SaveChanges();

        Profiling profiling = new Profiling
        {
            EmployeeId = entity.NIK,
            EducationId = education.Id
        };
        context.Profilings.Add(profiling);
        context.SaveChanges();

        context.Add(entity);
        result = context.SaveChanges();

        return result;
    }

    public bool Login(LoginVM entity)
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

        return getAccounts.Any(e => e.Email == entity.Email && e.Password == entity.Password);
    }
}
