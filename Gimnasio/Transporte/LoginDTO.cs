using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Transporte
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo de email es obligatorio")]
        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "Error en el formato del email introducido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de la constraseña es obligatorio.")]
        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).{10,20}$", ErrorMessage = "El formato de la contraseña es incorrecto.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
