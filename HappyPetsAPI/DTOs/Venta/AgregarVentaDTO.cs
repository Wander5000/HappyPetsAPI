namespace HappyPetsAPI.DTOs.Venta
{
    public class AgregarVentaDTO
    {
        public int Usuario { get; set; }

        public string MetodoPago { get; set; } = null!;

        public int Descuento { get; set; }

        public decimal Total { get; set; }

        public string? Observaciones { get; set; }

        public int Estado { get; set; }
    }

    public class AgregarDetalleDTO
    {
        public int Producto { get; set; }

        public int Cantidad { get; set; }
    }
}
