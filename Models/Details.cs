using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TisCircuitsAPI.Models;

public partial class Details
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    public DateOnly? date { get; set; }

    [StringLength(50)]
    public string? description { get; set; }

    public int FormationId { get; set; }

    [ForeignKey("FormationId")]
    [InverseProperty("Details")]
    [JsonIgnore]
    public virtual Formation? Formation { get; set; }


}
