using System.ComponentModel.DataAnnotations.Schema;

namespace WebProductsHardCode.Models.ProductModels;

[Table("Products")]
public class Product
{
    public int Id { get; set; }

    public string Image { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Count { get; set; }
    public int CategoryId { get; set; }
}
