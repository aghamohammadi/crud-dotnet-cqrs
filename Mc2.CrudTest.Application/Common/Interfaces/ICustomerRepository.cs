using Mc2.CrudTest.Domain.Entities;
using System.Linq.Expressions;

namespace Mc2.CrudTest.Application.Common.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);
    Task<Customer> GetByEmailAsync(string email);

    Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Customer>> GetAllAsync();
    IQueryable<Customer> GetAll(Expression<Func<Customer, bool>>? expression=null );

    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
    void DeleteRange(IEnumerable<Customer> customer);
}