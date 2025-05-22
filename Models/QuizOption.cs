using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TisCircuitsAPI.Models;


public class QuizOption
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    public int QuizQuestionId { get; set; }

    [ForeignKey("QuizQuestionId")]
    [JsonIgnore]
    public virtual QuizQuestion? QuizQuestion { get; set; }  // nullable

    [Required]
    public string option_text { get; set; } = null!;

    public bool is_correct { get; set; }
}

