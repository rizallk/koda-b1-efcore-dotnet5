using EfCore2.DTOs;

namespace EfCore2.Services;

public interface IProductQueryService
{
  Task<PagedResult<ProductSearchItemResponse>> SearchAsync(
      ProductSearchRequest request, CancellationToken ct
  );
}