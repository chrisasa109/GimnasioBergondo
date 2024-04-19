using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Models
{
    public class Carrito
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario _usuario;

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto _producto {  get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
