using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Nom { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [InverseProperty("Id_serviceNavigation")]
    public virtual ICollection<AccesFormation> AccesFormation { get; set; } = new List<AccesFormation>();

    [InverseProperty("Id_ServiceNavigation")]
    public virtual ICollection<User> User { get; set; } = new List<User>();
}
