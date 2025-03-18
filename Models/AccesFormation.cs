using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TisCircuitsAPI.Models;

public partial class AccesFormation
{
    [Key]
    public int Id { get; set; }

    public int? Id_formation { get; set; }

    public int? Id_service { get; set; }

    [ForeignKey("Id_formation")]
    [InverseProperty("AccesFormation")]
    public virtual Formation? Id_formationNavigation { get; set; }

    [ForeignKey("Id_service")]
    [InverseProperty("AccesFormation")]
    public virtual Service? Id_serviceNavigation { get; set; }
}
