using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TisCircuitsAPI.Models;

public class Matiere
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required]
	[StringLength(100)]
	public string Nom { get; set; } = null!;

	[Required]
	public int Quantite { get; set; }

	[Required]
	public DateOnly DateAjout { get; set; }
}
