using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TisCircuitsAPI.Models;

public partial class Type_Fourniture
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [StringLength(50)]
    public string nom { get; set; } = null!;

    [StringLength(50)]
    public string? qte { get; set; }

    // Supprimer la navigation inverse ici, sauf si tu veux la liste des fournitures par type
    // Si tu veux la liste des fournitures par type :
    [InverseProperty("Type_Fourniture")]
    public virtual ICollection<Fourniture> Fournitures { get; set; } = new List<Fourniture>();

}

