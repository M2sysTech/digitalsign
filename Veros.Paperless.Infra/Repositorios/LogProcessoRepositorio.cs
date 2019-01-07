namespace Veros.Paperless.Infra.Repositorios
{
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class LogProcessoRepositorio : Repositorio<LogProcesso>, ILogProcessoRepositorio
    {
        public int ObterPorProcesso(int processoId)
        {
            var listaAcao = new[]
            {
                LogProcesso.AcaoDevolverNaAprovacao,
                LogProcesso.AcaoEncerrarNaAprovacao,
                LogProcesso.AcaoLiberarNaAprovacao
            };
            return this.Session.QueryOver<LogProcesso>()
                .Where(x => x.Processo.Id == processoId)
                .WhereRestrictionOn(x => x.Acao).IsIn(listaAcao)
                .RowCount();
        }
    }
}