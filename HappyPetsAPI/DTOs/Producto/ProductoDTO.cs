using System.Collections;

namespace HappyPetsAPI.DTOs.Producto
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;

        public string CategoriaProducto { get; set; }

        public decimal PrecioUnidad { get; set; }

        public int Stock { get; set; }

        public bool Estado { get; set; }

        public List<ImagenDTO> Imagenes { get; set; } = new List<ImagenDTO>();
    }

    public class ImagenDTO
    {
        public int IdImagen { get; set; }
        public string Url { get; set; } = null!;
    }
}
