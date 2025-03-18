using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class Role
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string nom { get; set; } = null!;

    [InverseProperty("role")]
    public virtual ICollection<User> User { get; set; } = new List<User>();
}
