using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Models
{
    public class Actividad
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Descripción")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Debe tener un mínimo de 6 caracter y un máximo de 50.")]
        [DataType(DataType.Text)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Duración")]
        [DataType(DataType.Time)]
        public TimeOnly Duracion { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Capacidad máxima")]
        public int CapacidadMaxima { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha y hora")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }
    }
}