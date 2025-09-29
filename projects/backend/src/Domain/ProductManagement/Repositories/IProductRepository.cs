using Domain.ProductManagement.ValueObjects;
using Shared.Base.Interfaces;
using Shared.ValueObjects;

namespace Domain.ProductManagement.Repositories
{
    public interface IProductRepository : IBaseRepository<Product, IdProduct>
    {
        public Task<SerialNumber> GetNextSerialNumberAsync(CancellationToken cancellationToken);

    }
}
