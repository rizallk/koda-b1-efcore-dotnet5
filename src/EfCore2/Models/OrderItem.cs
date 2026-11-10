using EfCore1.Models;

namespace EfCore2.Models;

public sealed class OrderItem
{
  private OrderItem() { }
  public long OrderItemId { get; private set; }

  public long OrderId { get; private set; }
  public Order Order { get; private set; } = null!;
  public string ProductCode { get; private set; } = string.Empty;
  public Product Product { get; private set; } = null!;
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }
  public decimal Subtotal { get; private set; }

  public OrderItem(Product product, int quantity)
  {
    ArgumentNullException.ThrowIfNull(product);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

    ProductCode = product.ProductCode;
    Product = product;
    Quantity = quantity;
    UnitPrice = product.Price;
  }
}