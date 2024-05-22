using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface ITarifaRepository
    {
        Task<List<TarifaDTO>> ConsultaTarifas();
    }
}
