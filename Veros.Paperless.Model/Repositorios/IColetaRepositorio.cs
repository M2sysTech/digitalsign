namespace Veros.Paperless.Model.Repositorios
{
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IColetaRepositorio : IRepositorio<Coleta>
    {
        IList<Coleta> ObterPorUsuarioRegistro(int usuarioId);

        IList<Coleta> ObterPendentes();

        Coleta ObterComPacotesPorId(int id);

        IList<Coleta> ObterAgendadaComTransportadora();

        void AtualizarArquivo(int coletaId, string arquivo);

        IList<Coleta> ObterParaImportar();

        void AtualizarStatus(int coletaId, ColetaStatus status);
    }
}