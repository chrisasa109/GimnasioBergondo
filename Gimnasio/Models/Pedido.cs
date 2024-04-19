using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Gimnasio.Models.Usuario;

namespace Gimnasio.Models
{
    public class Pedido
    {
        [Key]
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

        [ForeignKey("Usuario")]
        public int UsuarioID { get; set; }
        public Usuario _usuario { get; set; }
    }
}
