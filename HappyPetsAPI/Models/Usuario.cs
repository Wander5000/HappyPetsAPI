using System;
using System.Collections.Generic;

namespace HappyPetsAPI.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string TipoDocumento { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int Rol { get; set; }

    public bool Estado { get; set; }

    public virtual Rol RolNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
