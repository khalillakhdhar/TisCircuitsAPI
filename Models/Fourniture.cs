using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class Fourniture
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? title { get; set; }

    [StringLength(50)]
    public string? description { get; set; }

    public int? quantite { get; set; }

    public DateOnly? date_creation { get; set; }

    [StringLength(50)]
    public string? matricule_demandeur { get; set; }

    [StringLength(50)]
    public string? etats { get; set; }

    [StringLength(50)]
    public string? type { get; set; }

    [InverseProperty("idNavigation")]
    public virtual Type_Fourniture? Type_Fourniture { get; set; }
}
