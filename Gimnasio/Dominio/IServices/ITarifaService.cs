using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface ITarifaService
    {
        Task<List<TarifaDTO>> ConsultaTarifas();
    }
}
