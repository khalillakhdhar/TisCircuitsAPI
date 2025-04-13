using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TisCircuitsAPI.Models;

public class DemandeConge
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DateDebut { get; set; }

    [Required]
    public DateTime DateFin { get; set; }

    [Required]
    [StringLength(50)]
    public string Etat { get; set; } = "En attente"; // En attente, Acceptée, Refusée

    [Required]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}
