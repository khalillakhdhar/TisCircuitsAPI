using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TisCircuitsAPI.Models;

public class Quiz
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int id { get; set; }

	[Required, StringLength(200)]
	public string titre { get; set; } = null!;

	public int FormationId { get; set; }

	[ForeignKey("FormationId")]
	[JsonIgnore]
	public virtual Formation? Formation { get; set; }  // nullable to avoid validation error

	public DateTime date_creation { get; set; } = DateTime.UtcNow;

	public virtual ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
}