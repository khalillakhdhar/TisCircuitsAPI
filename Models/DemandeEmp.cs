using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TisCircuitsAPI.Models;

public partial class DemandeEmp
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [StringLength(100)]
    public string nom { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? description { get; set; }

    [InverseProperty("id_fichierNavigation")]
    public virtual ICollection<Demande> Demande { get; set; } = new List<Demande>();
}
