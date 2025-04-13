using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TisCircuitsAPI.Models;

public partial class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    [StringLength(50)]
    public string nom { get; set; } = null!;

    [InverseProperty("role")]
    [JsonIgnore]
    public virtual ICollection<User> User { get; set; } = new List<User>();

}
