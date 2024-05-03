using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Models
{
    public class DetallePedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("Pedido")]
        public int PedidoId { get; set;}
        public Pedido _pedido {  get; set; }
        
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto _producto { get; set; }
        
        [Required(ErrorMessage ="El campo no puede quedar vacío.")]
        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

    }
}
