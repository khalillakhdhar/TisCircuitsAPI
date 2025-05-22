using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TisCircuitsAPI.Models;
public class QuizResult
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }

    public int QuizId { get; set; }

    [ForeignKey("QuizId")]
    [JsonIgnore]
    public virtual Quiz? Quiz { get; set; }  // nullable

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    [JsonIgnore]
    public virtual User? User { get; set; }  // nullable

    public int score { get; set; }

    public DateTime date_taken { get; set; } = DateTime.UtcNow;
}