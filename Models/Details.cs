using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class Details
{
    [Key]
    public int id { get; set; }

    public DateOnly? date { get; set; }

    [StringLength(50)]
    public string? description { get; set; }

    [ForeignKey("id")]
    [InverseProperty("Details")]
    public virtual Formation idNavigation { get; set; } = null!;
}
