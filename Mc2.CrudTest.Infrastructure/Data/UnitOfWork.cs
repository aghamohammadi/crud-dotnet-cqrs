using Mc2.CrudTest.Application.Common.Interfaces;
using Mc2.CrudTest.Infrastructure.Repositories;

namespace Mc2.CrudTest.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ICustomerRepository _customerRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _customerRepository = new CustomerRepository(_context);
    }

    public ICustomerRepository Customers
        => _customerRepository ??= new CustomerRepository(_context);


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
