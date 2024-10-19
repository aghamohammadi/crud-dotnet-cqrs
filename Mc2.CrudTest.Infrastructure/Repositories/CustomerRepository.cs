using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mc2.CrudTest.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.FindAsync(new object[] { id }, cancellationToken);

    }
    public async Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Customers.AnyAsync(predicate, cancellationToken);
    }

    public async Task<Customer> GetByEmailAsync(string email)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
    }
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }

    public IQueryable<Customer> GetAll(Expression<Func<Customer, bool>>? expression=null)
    {
        var result = _context.Customers.AsNoTracking();
        if(expression != null)
        {
            result=result.Where(expression);
        }

        return result;
    }


    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public void Delete(Customer customer)
    {
        _context.Customers.Remove(customer);
    }

    public void DeleteRange(IEnumerable<Customer> customer)
    {
        _context.Customers.RemoveRange(customer);
    }

}
