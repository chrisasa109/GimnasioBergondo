
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

/*
 * Esta clase representa la relación entre el usuario y las actividades.
 */
namespace Gimnasio.Models
{
    public class UsuarioActividad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario? _usuario {  get; set; }
        
        [ForeignKey("Actividad")]
        public int ActividadId { get; set; }
        public virtual Actividad? Actividad { get; set; }

        [Display(Name = "Notas")]
        [DataType(DataType.MultilineText)]
        [AllowNull]
        [StringLength(500, ErrorMessage = "Las notas no pueden superar los 500 caracteres.")]
        public string? Notas {  get; set; }
    }
}
