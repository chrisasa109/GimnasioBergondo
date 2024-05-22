using Gimnasio.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Transporte
{
    public class ContratoDTO
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioDTO? _usuario { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateOnly FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha de fin")]
        [DataType(DataType.Date)]
        public DateOnly FechaFin { get; set; }

        [Display(Name = "Comentarios")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Los comentarios no pueden superar los 500 caracteres.")]
        public string? Comentarios { get; set; }

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

        public int TarifaID { get; set; }
        public TarifaDTO? _tarifa { get; set; }
    }
}
