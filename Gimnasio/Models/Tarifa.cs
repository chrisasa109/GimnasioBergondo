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
