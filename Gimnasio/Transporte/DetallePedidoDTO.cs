using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Transporte
{
    public class DetallePedidoDTO
    {
        public int Id { get; set; }

        public int PedidoId { get; set; }
        public PedidoDTO _pedido { get; set; }

        public int ProductoId { get; set; }
        public ProductoDTO _producto { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
    }
}
