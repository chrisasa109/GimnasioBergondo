using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Transporte
{
    public class CarritoDTO
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioDTO _usuario;

        public int ProductoId { get; set; }
        public ProductoDTO _producto { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
