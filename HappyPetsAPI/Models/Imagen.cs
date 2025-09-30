using System;
using System.Collections.Generic;

namespace HappyPetsAPI.Models;

public partial class Imagen
{
    public int IdImagen { get; set; }

    public int Producto { get; set; }

    public string Url { get; set; } = null!;

    public virtual Producto ProductoNavigation { get; set; } = null!;
}
