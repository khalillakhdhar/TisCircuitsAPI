using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class Type_Fourniture
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string nom { get; set; } = null!;

    [StringLength(50)]
    public string? qte { get; set; }

    [ForeignKey("id")]
    [InverseProperty("Type_Fourniture")]
    public virtual Fourniture idNavigation { get; set; } = null!;
}
