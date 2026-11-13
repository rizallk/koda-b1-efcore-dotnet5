using EfCore2.DTOs;

namespace EfCore2.Repositories;

public interface IProductQueryRepository
{
  Task<PagedResult<ProductSearchItemResponse>> SearchAsync(ProductSearchCriteria criteria, CancellationToken ct);
}