using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TisCircuitsAPI.Models;

public partial class Fourniture
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    // 🔄 Ce champ doit être de type int pour fonctionner comme FK
    public int? TypeFournitureId { get; set; }

    [ForeignKey("TypeFournitureId")]
    public virtual Type_Fourniture? Type_Fourniture { get; set; }

    public int? MatiereId { get; set; }

    [ForeignKey("MatiereId")]
    public virtual Matiere? Matiere { get; set; }
}
