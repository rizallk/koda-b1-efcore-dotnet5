using EfCore2.DTOs;
using EfCore2.Services;
using Microsoft.AspNetCore.Mvc;

namespace EfCore2.Controllers;

[ApiController]
[Route("api/v1/products")]
public sealed class ProductQueryController(IProductQueryService service) : ControllerBase
{
  private readonly IProductQueryService _service = service;

  [HttpGet("search")]

  [ProducesResponseType(typeof(PagedResult<ProductSearchItemResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(PagedResult<ProductSearchItemResponse>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(PagedResult<ProductSearchItemResponse>), StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<PagedResult<ProductSearchItemResponse>>> Search(
      [FromQuery] ProductSearchRequest request, CancellationToken ct
  )
  {
    var response = await _service.SearchAsync(
        request, ct
    );

    return Ok(response);
  }
}