namespace Mc2.CrudTest.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICustomerRepository Customers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}