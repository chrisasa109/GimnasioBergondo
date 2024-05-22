using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IContratoRepository
    {
        ContratoDTO ComprobarContrato(int idUsuario);
        Task<bool> ContratoActivado(int usuarioId);
        Task<ContratoDTO> ContratoPrevio(int tarifaId);
        Task<bool> ContratoVigente();
        Task<bool> GuardarCambiosNotas(string c);
        Task<ContratoDTO> ObtenerContrato();
        Task<bool> RealizarContrato(ContratoDTO contratoFronted);
    }
}
