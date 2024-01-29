using System.ComponentModel.DataAnnotations.Schema;

namespace WebProductsHardCode.Models.CategoryModels;

[Table("Categories")]
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; } 
}
