using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Models
{
    public class Login
    {
        [Required(ErrorMessage = "El campo de email es obligatorio")]
        [EmailAddress(ErrorMessage ="Error en el formato del email introducido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de la constraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
