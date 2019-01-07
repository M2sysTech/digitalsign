namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface ILogDocumentoRepositorio : IRepositorio<LogDocumento>
    {
        int ObterTotalPorId(int p);

        int ObterTotalPorIdEmDigitacao(int id);

        int ObterQuantidadeDoDia(int usuarioId, string logAcao, DateTime data);

        IList<LogDocumento> ObterPorDocumentoId(int documentoId);

        IList<LogDocumento> ObterPorLote(Lote lote, TipoDocumento tipoDocumento);

        IList<LogDocumento> ObterHistoricoDoGrupo(Lote lote, TipoDocumento tipoDocumento, string cpf);
    }
}
