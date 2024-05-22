using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Transporte
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [RegularExpression(@"^\d{8}[A-Za-z]$", ErrorMessage = "El formato del DNI no es válido.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener un mínimo de 3 caracteres y un máximo de 50.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Debe tener un mínimo de 6 caracteres y un máximo de 50.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "El valor introducido no es una fecha válida.")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Población")]
        public string Poblacion { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Phone(ErrorMessage = "Teléfono inválido")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).{10,20}$", ErrorMessage = "La contraseña debe tener al menos un número, una mayúscula, una minúscula y estar entre 10 y 20 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [EnumDataType(typeof(Role))]
        public Role Rol { get; set; } = Role.CLIENTE;

        public byte[]? Foto { get; set; }
        public IFormFile? FotoFronted { get; set; }

        public enum Role
        {
            ADMINISTRADOR,
            TRABAJADOR,
            CLIENTE
        }
    }
}
