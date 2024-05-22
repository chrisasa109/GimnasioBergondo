using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IContratoService
    {
        ContratoDTO ComprobarContrato(int idUsuario);
        Task<bool> ContratoActivado(int usuarioId);
        Task<ContratoDTO> ContratoPrevio(int tarifaId);
        Task<bool> ContratoVigente();
        Task<bool> GuardarCambiosNotas(string comentarios);
        Task<ContratoDTO> ObtenerContrato();
        Task<bool> RealizarContrato(ContratoDTO contratoFronted);
    }
}
