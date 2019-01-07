namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class LogDocumentoRepositorio : Repositorio<LogDocumento>, ILogDocumentoRepositorio
    {
        public int ObterTotalPorId(int p)
        {
            return this.Session.QueryOver<LogDocumento>()
                .Where(x => x.Acao == LogDocumento.AcaoDocumentoProvazero)
                .RowCount();
        }

        public int ObterTotalPorIdEmDigitacao(int id)
        {
            return this.Session.QueryOver<LogDocumento>()
                .Where(x => x.Acao == LogDocumento.AcaoDocumentoDigitacao)
                .RowCount();
        }

        public int ObterQuantidadeDoDia(int usuarioId, string logAcao, DateTime data)
        {
            return this.Session.QueryOver<LogDocumento>()
                .Where(x => x.Acao == logAcao)
                .And(x => x.Usuario.Id == usuarioId)
                .AndRestrictionOn(x => x.DataRegistro).IsBetween(data).And(data.AddDays(1))
                .RowCount();
        }

        public IList<LogDocumento> ObterPorDocumentoId(int documentoId)
        {
            return this.Session.QueryOver<LogDocumento>()
                .Where(x => x.Documento.Id == documentoId)
                .List();
        }

        public IList<LogDocumento> ObterPorLote(Lote lote, TipoDocumento tipoDocumento)
        {
            return this.Session.QueryOver<LogDocumento>()
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Lote == lote)
                .And(x => x.TipoDocumento == tipoDocumento)
                .List();
        }

        public IList<LogDocumento> ObterHistoricoDoGrupo(Lote lote, TipoDocumento tipoDocumento, string cpf)
        {
            return this.Session.QueryOver<LogDocumento>()
                .JoinQueryOver(x => x.Documento)
                .Where(x => x.Lote == lote)
                .And(x => x.TipoDocumento == tipoDocumento)
                .And(x => x.Cpf == cpf)
                .List();
        }
    }
}