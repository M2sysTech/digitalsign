namespace Veros.Paperless.Infra.Repositorios
{
    using System.Linq;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class RecepcaoRepositorio : Repositorio<Recepcao>, IRecepcaoRepositorio
    {
        public Recepcao[] ObterPendente()
        {
            return this.Session.QueryOver<Recepcao>()
                .WhereRestrictionOn(x => x.Status).In(RecepcaoStatus.Pendente, RecepcaoStatus.Iniciado)
                .OrderBy(x => x.CadastradoEm).Asc
                .List()
                .ToArray();
        }

        public void IncrementarImportado(Recepcao recepcao)
        {
            recepcao.QuantidadeImportado++;
            this.Salvar(recepcao);
        }
    }
}
