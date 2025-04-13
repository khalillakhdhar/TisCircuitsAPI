using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TisCircuitsAPI.Models;

public partial class Demande
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [StringLength(100)]
    public string? title { get; set; }

    [Column(TypeName = "text")]
    public string? description { get; set; }

    [StringLength(50)]
    public string? matricule { get; set; }

    public DateOnly datecreation { get; set; }

    public DateOnly? daterecu { get; set; }

    [StringLength(50)]
    public string? matriculerecu { get; set; }

    [StringLength(50)]
    public string? etat { get; set; }

    public int? id_fichier { get; set; }

    [ForeignKey("id_fichier")]
    [InverseProperty("Demande")]
    public virtual DemandeEmp? id_fichierNavigation { get; set; }
}
