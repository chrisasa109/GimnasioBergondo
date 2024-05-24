using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Gimnasio.Transporte
{
    public class UsuarioActividadDTO
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public virtual UsuarioDTO _usuario { get; set; }

        public int ActividadId { get; set; }
        public virtual ActividadDTO Actividad { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        [AllowNull]
        [StringLength(500, ErrorMessage = "Las notas no pueden superar los 500 caracteres.")]
        public string? Notas { get; set; }
    }
}
