using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Gimnasio.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [RegularExpression(@"^\d{8}[A-Za-z]$", ErrorMessage = "El formato del DNI no es válido.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Debe tener un mínimo de 3 caracteres y un máximo de 50.")]
        [Display(Name = "Nombre")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Debe tener un mínimo de 6 caracter y un máximo de 50.")]
        [Display(Name = "Apellidos")]
        [DataType(DataType.Text)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date, ErrorMessage = "El valor introducido no es una fecha válida.")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Dirección")]
        [DataType(DataType.Text)]
        public string Direccion {  get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Población")]
        [DataType(DataType.Text)]
        public string Poblacion { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Teléfono")]
        [Phone(ErrorMessage ="Teléfono inválido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).{10,20}$", ErrorMessage = "La contraseña debe tener al menos un número, una mayúscula, una minúscula y estar entre 10 y 20 caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Repetir contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "El campo no puede quedar vacío.")]
        [EnumDataType(typeof(Role))]
        [Display(Name = "Rol del usuario")]
        public Role Rol {  get; set; } = Role.CLIENTE;

        [Display(Name = "Foto de perfil")]
        public byte[] ? Foto { get; set; }
        
        [NotMapped]
        public IFormFile ? FotoFronted { get; set; }
        public enum Role
        {
            ADMINISTRADOR,
            TRABAJADOR,
            CLIENTE
        }
    }
}
