using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class DemandeEmp
{
    [Key]
    public int id { get; set; }

    [StringLength(100)]
    public string nom { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? description { get; set; }

    [InverseProperty("id_fichierNavigation")]
    public virtual ICollection<Demande> Demande { get; set; } = new List<Demande>();
}
