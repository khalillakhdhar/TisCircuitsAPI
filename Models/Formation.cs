using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class Formation
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string titre { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? description { get; set; }

    public DateOnly date_creation { get; set; }

    [StringLength(50)]
    public string? etat { get; set; }

    [StringLength(50)]
    public string? type { get; set; }

    [StringLength(50)]
    public string? details { get; set; }

    [InverseProperty("Id_formationNavigation")]
    public virtual ICollection<AccesFormation> AccesFormation { get; set; } = new List<AccesFormation>();

    [InverseProperty("idNavigation")]
    public virtual Details? Details { get; set; }
}
