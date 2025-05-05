using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TisCircuitsAPI.Models;

public class Cours
{
    public int Id { get; set; }

    [Required]
    public string Titre { get; set; }

    [Required]
    public string UrlFichier { get; set; }

    [ForeignKey("Formation")]
    public int FormationId { get; set; }

    [JsonIgnore]
    public Formation Formation { get; set; }
}
