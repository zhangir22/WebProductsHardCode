using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HardCodeWebApi.Models;

[Table("Categories")]
public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Additional { get; set; } 


}
