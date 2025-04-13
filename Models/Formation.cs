using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TisCircuitsAPI.Models;

public partial class Formation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

 

    [InverseProperty("Id_formationNavigation")]
    public virtual ICollection<AccesFormation> AccesFormation { get; set; } = new List<AccesFormation>();

    [InverseProperty("Formation")]
    public virtual ICollection<Details> Details { get; set; } = new List<Details>();

}
