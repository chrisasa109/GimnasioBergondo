using System.Diagnostics.CodeAnalysis;

namespace Gimnasio.Transporte.UsuarioActividad
{
    public class ModeloUserAct
    {
        public int ActividadId { get; set; }
        [AllowNull]
        public string? Notas { get; set; }
    }
}
