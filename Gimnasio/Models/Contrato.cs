using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Models
{
    /*
     * Esta clase representa la relación entre las tablas Usuario y Tarifa
     */
    public class Contrato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario? _usuario {  get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateOnly FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha de fin")]
        [DataType(DataType.Date)]
        public DateOnly FechaFin {  get; set; }

        [Display(Name = "Comentarios")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Los comentarios no pueden superar los 500 caracteres.")]
        public string? Comentarios {  get; set; }

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

        [ForeignKey("Tarifa")]
        public int TarifaID { get; set; }
        public virtual Tarifa? _tarifa { get; set; }
    }
}
