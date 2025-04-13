using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TisCircuitsAPI.Models;

public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [StringLength(50)]
    public string? matricule { get; set; }
    [Required]
    [StringLength(100)]
    public string nom_complet { get; set; } = null!;

    [StringLength(50)]
    public string? matriculeWindows { get; set; }

    [StringLength(100)]
    public string? email { get; set; }

    public int? role_id { get; set; }

    [StringLength(100)]
    public string? fonction { get; set; }

    [StringLength(50)]
    public string? responsable { get; set; }

    public int? Id_Service { get; set; }

    public int? Etats { get; set; }

    [ForeignKey("Id_Service")]
    [InverseProperty("User")]
    public virtual Service? Id_ServiceNavigation { get; set; }

    [ForeignKey("role_id")]
    [InverseProperty("User")]
    public virtual Role? role { get; set; }
}
