using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardCodeWebApi.Models;

[Table("Products")]
public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Count { get; set; }
    public string? AdditionalValues { get; set; }
    public int CategoryId { get; set; }
     
        
}
