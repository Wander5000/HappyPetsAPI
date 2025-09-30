using System;
using System.Collections.Generic;

namespace HappyPetsAPI.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int CategoriaProducto { get; set; }

    public decimal PrecioUnidad { get; set; }

    public int Stock { get; set; }

    public bool Estado { get; set; }

    public virtual Categoria CategoriaProductoNavigation { get; set; } = null!;

    public virtual ICollection<DetallesVenta> DetallesVenta { get; set; } = new List<DetallesVenta>();

    public virtual ICollection<Imagen> Imagenes { get; set; } = new List<Imagen>();
}
