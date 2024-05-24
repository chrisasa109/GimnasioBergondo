using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Transporte
{
    public class PedidoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha de pedido")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [EnumDataType(typeof(Metodo))]
        [Display(Name = "Forma de pago")]
        public Metodo FormaPago { get; set; }

        public enum Metodo
        {
            EFECTIVO,
            TARJETA,
            TRANSFERENCIA
        }

        public List<DetallePedidoDTO> DetallesPedido { get; set; }

        [NotMapped]
        public double PrecioTotal { get; set; }

        public int UsuarioID { get; set; }
        public UsuarioDTO _usuario { get; set; }
    }
}
