using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class TarifaService : ITarifaService
    {
        private ITarifaRepository _ITarifaRepository;
        public TarifaService(ITarifaRepository iTarifaRepository)
        {
            _ITarifaRepository = iTarifaRepository;
        }

        async Task<List<TarifaDTO>> ITarifaService.ConsultaTarifas()
        {
            return await _ITarifaRepository.ConsultaTarifas();
        }
    }
}
