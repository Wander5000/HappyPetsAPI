using System;
using System.Collections.Generic;

namespace HappyPetsAPI.Models;

public partial class Estado
{
    public int IdEstado { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
