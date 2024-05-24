using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IUsuarioActividadRepository
    {
        Task<List<UsuarioActividadDTO>> ObtenerActividadesDelUsuarioPorId(int? id);
    }
}
