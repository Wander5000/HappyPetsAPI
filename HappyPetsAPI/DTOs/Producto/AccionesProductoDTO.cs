using System.Collections;

namespace HappyPetsAPI.DTOs.Producto
{
    public class AccionesProductoDTO
    {
        public string NombreProducto { get; set; } = null!;

        public int CategoriaProducto { get; set; }

        public decimal PrecioUnidad { get; set; }

        public int Stock { get; set; }

        public bool Estado { get; set; }

        public List<AccionesImagenDTO> Imagenes { get; set; } = new List<AccionesImagenDTO>();
    }

    public class AccionesImagenDTO
    {
        public string Url { get; set; } = null!;
    }
}
