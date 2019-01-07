namespace Veros.Paperless.Infra.Repositorios
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Repositorios;

    public class ArquivoColetaRepositorio : Repositorio<ArquivoColeta>, IArquivoColetaRepositorio
    {
        public ArquivoColeta ObterUltimo(int coletaId)
        {
            var arquivos = this.Session.QueryOver<ArquivoColeta>()
                .Where(x => x.Coleta.Id == coletaId)
                .Fetch(x => x.Coleta).Eager
                .Fetch(x => x.UsuarioUpado).Eager
                .List();

            return arquivos.OrderBy(x => x.Id).LastOrDefault();
        }

        public ArquivoColeta ObterUltimo(int coletaId, string nomeArquivo)
        {
            var arquivos = this.Session.QueryOver<ArquivoColeta>()
                .Where(x => x.Coleta.Id == coletaId)
                .Fetch(x => x.Coleta).Eager
                .Fetch(x => x.UsuarioUpado).Eager
                .List();

            return arquivos.Where(x => x.NomeArquivo.ToUpper() == nomeArquivo.ToUpper()).OrderBy(x => x.Id).LastOrDefault();
        }

        public void FinalizarPendentes(int coletaId)
        {
            this.Session
              .CreateQuery("update ArquivoColeta set Status = :statusNovo where Status = :statusAtual and Coleta.Id = :coletaId")
              .SetParameter("statusNovo", ArquivoColeta.Descartado)
              .SetParameter("statusAtual", ArquivoColeta.AguardandoAnalise)
              .SetParameter("coletaId", coletaId)
              .ExecuteUpdate();
        }

        public void AlterarStatus(ArquivoColeta arquivo, string status)
        {
            this.Session
              .CreateQuery("update ArquivoColeta set Status = :statusNovo where Id = :id")
              .SetParameter("statusNovo", status)
              .SetParameter("id", arquivo.Id)
              .ExecuteUpdate();
        }
    }
}