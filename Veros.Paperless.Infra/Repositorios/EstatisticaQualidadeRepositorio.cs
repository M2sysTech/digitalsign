namespace Veros.Paperless.Infra.Repositorios
{
    using System;
    using Veros.Data.Hibernate;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class EstatisticaQualidadeRepositorio : Repositorio<EstatisticaQualidade>, IEstatisticaQualidadeRepositorio
    {
        public override void Salvar(EstatisticaQualidade item)
        {
            base.Salvar(item);
            this.Session.Flush();
        }

        public EstatisticaQualidade ObterDeHojePorUsuario(int usuarioId)
        {
            return this.Session.QueryOver<EstatisticaQualidade>()
               .Where(x => x.Usuario.Id == usuarioId)
               .Where(x => x.DataRegistro == DateTime.Today)
               .Take(1)
               .SingleOrDefault();
        }

        public void IncrementarOkParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaQualidade set 
TotalOk = TotalOk + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalOk: 1);
            }
        }

        public void IncrementarNaoOkParaHoje(int usuarioId)
        {
            var linhasAfetadas = this.Session
               .CreateQuery(@"
update EstatisticaQualidade set 
TotalNaoOk = TotalNaoOk + 1 
where Usuario.Id = :usuarioId
and DataRegistro = trunc(current_date())
")
               .SetInt32("usuarioId", usuarioId)
               .ExecuteUpdate();

            if (linhasAfetadas <= 0)
            {
                this.IncluirEstatistica(usuarioId, totalNaoOk: 1);
            }
        }
        
        private void IncluirEstatistica(int usuarioId, 
                                        int totalOk = 0, 
                                        int totalNaoOk = 0)
        {
            var estatisticaQualidade = new EstatisticaQualidade
            {
                TotalOk = totalOk,
                TotalNaoOk = totalNaoOk,
                Usuario = new Usuario { Id = usuarioId }
            };

            this.Salvar(estatisticaQualidade);
        }
    }
}