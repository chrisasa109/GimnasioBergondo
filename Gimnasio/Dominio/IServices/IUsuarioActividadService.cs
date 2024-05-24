using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IUsuarioActividadService
    {
        Task<List<UsuarioActividadDTO>> ObtenerActividadesDelUsuarioPorId(int? id);
    }
}
