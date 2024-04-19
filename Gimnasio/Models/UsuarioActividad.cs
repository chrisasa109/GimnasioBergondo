using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 * Esta clase representa la relación entre el usuario y las actividades.
 */
namespace Gimnasio.Models
{
    public class UsuarioActividad
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        
        [ForeignKey("Actividad")]
        public string ActividadId { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha y hora de realización")]
        public DateTime FechaRealizacion { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Las notas no pueden superar los 500 caracteres.")]
        public string Notas {  get; set; }
    }
}
