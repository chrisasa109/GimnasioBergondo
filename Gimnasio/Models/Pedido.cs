using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Models
{
    [Authorize]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public List<DetallePedido> DetallesPedido { get; set; }

        [NotMapped]
        public double PrecioTotal { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioID { get; set; }
        public Usuario _usuario { get; set; }
    }
}
