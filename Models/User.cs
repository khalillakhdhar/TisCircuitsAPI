using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

[Index("matricule", Name = "UQ__User__30962D11A75D479D", IsUnique = true)]
[Index("email", Name = "UQ__User__AB6E6164437C7468", IsUnique = true)]
[Index("matriculeWindows", Name = "UQ__User__EBFD5DD5015023F6", IsUnique = true)]
public partial class User
{
    [Key]
    public int id { get; set; }

    [StringLength(50)]
    public string? matricule { get; set; }

    [StringLength(50)]
    public string? matriculeWindows { get; set; }

    [StringLength(100)]
    public string? email { get; set; }

    public int? role_id { get; set; }

    [StringLength(100)]
    public string? fonction { get; set; }

    [StringLength(50)]
    public string? responsable { get; set; }

    public int? Id_Service { get; set; }

    public int? Etats { get; set; }

    [ForeignKey("Id_Service")]
    [InverseProperty("User")]
    public virtual Service? Id_ServiceNavigation { get; set; }

    [ForeignKey("role_id")]
    [InverseProperty("User")]
    public virtual Role? role { get; set; }
}
