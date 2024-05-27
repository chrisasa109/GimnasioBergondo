using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class ContratoService : IContratoService
    {
        private readonly IContratoRepository _IContratoRepository;
        private readonly SessionService _SessionService;
        public ContratoService(IContratoRepository contratoRepository, SessionService sessionService)
        {
            _IContratoRepository = contratoRepository;
            _SessionService = sessionService;
        }

        public ContratoDTO ComprobarContrato(int idUsuario)
        {
            return _IContratoRepository.ComprobarContrato(idUsuario);
        }

        public Task<bool> ContratoActivado(int? usuarioId)
        {
            usuarioId ??= _SessionService.ObtenerUsuario().Id;
            return _IContratoRepository.ContratoActivado((int)usuarioId);
        }

        public async Task<ContratoDTO> ContratoPrevio(int tarifaId)
        {
            return await _IContratoRepository.ContratoPrevio(tarifaId);
        }

        public async Task<bool> ContratoVigente()
        {
            return await _IContratoRepository.ContratoVigente();
        }

        public async Task<bool> GuardarCambiosNotas(string c)
        {
            return await _IContratoRepository.GuardarCambiosNotas(c);
        }

        public async Task<ContratoDTO> ObtenerContrato()
        {
            return await _IContratoRepository.ObtenerContrato();
        }

        public async Task<bool> RealizarContrato(ContratoDTO contratoFronted)
        {
            return await _IContratoRepository.RealizarContrato(contratoFronted);
        }
    }
}
