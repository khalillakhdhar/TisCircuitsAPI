using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TisCircuitsAPI.Models;

public class QuizQuestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    public int QuizId { get; set; }

    [ForeignKey("QuizId")]
    [JsonIgnore]
    public virtual Quiz? Quiz { get; set; }  // nullable

    [Required]
    public string question_text { get; set; } = null!;

    public virtual ICollection<QuizOption> Options { get; set; } = new List<QuizOption>();
}

