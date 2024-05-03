using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Models
{
    public class Tarifa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name= "Descripción")]
        [StringLength(1000, ErrorMessage ="La descripción no puede superar los 500 caracteres.")]
        public string Descripcion { get; set; }
        
        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Precio")]
        [Range(0.00, double.MaxValue, ErrorMessage = "El valor de precio no admite valores negativos.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe tener máximo dos decimales.")]
        [DataType(DataType.Currency)]
        public double Precio { get; set; }
    }
}

/*
 * Tarifa Básica 29,99
 * Acceso completo al gimnasio durante las horas de operación estándar. Incluye acceso a todas las áreas de entrenamiento, como la sala de pesas, las clases grupales y las instalaciones de cardio. Los miembros también pueden utilizar los vestuarios y las duchas.
 * Tarifa Premium 49.99
 * Esta tarifa ofrece todas las ventajas de la tarifa básica, además de acceso exclusivo a clases premium y entrenamientos personalizados. Los miembros de esta tarifa también tienen acceso prioritario a ciertos equipos y pueden reservar clases con antelación.
 * Tarifa Familiar 69.99
 * Diseñada para familias que desean entrenar juntas, esta tarifa ofrece acceso completo al gimnasio para dos adultos y hasta dos niños menores de 18 años. Los miembros de la tarifa familiar también pueden participar en clases grupales juntos y disfrutar de descuentos en servicios adicionales, como entrenamiento personalizado o programas de nutrición.
 */
