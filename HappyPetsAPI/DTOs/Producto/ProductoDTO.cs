using System.Collections;

namespace HappyPetsAPI.DTOs.Producto
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;

        public int CategoriaProducto { get; set; }

        public decimal PrecioUnidad { get; set; }

        public int Stock { get; set; }

        public BitArray? Estado { get; set; }
    }
}
