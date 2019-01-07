namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework.Servicos;
    using Repositorios;

    public class AtualizaLoteCefNoControleDeQualidadeCefServico : IAtualizaLoteCefNoControleDeQualidadeCefServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly ILoteCefRepositorio loteCefRepositorio;
        private readonly IGravaLogGenericoServico gravaLogGenericoServico;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;

        public AtualizaLoteCefNoControleDeQualidadeCefServico(
            ISessaoDoUsuario userSession,
            ILoteCefRepositorio loteCefRepositorio, 
            IGravaLogGenericoServico gravaLogGenericoServico, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico)
        {
            this.userSession = userSession;
            this.loteCefRepositorio = loteCefRepositorio;
            this.gravaLogGenericoServico = gravaLogGenericoServico;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
        }

        public void Executar(int loteCefId, string acao, string observacao)
        {
            switch (acao)
            {
                case "A":
                    this.gravaLogGenericoServico.Executar(LogGenerico.AcaoAprovarLoteCef, loteCefId, observacao, "QualidadeCEF", this.userSession.UsuarioAtual.Login);
                    this.loteCefRepositorio.Aprovar(loteCefId, this.userSession.UsuarioAtual.Id);
                    this.AlterarStatusDossies(loteCefId);
                    break;

                case "M":
                    this.gravaLogGenericoServico.Executar(LogGenerico.AcaoReprovarLoteCef, loteCefId, observacao, "QualidadeCEF", this.userSession.UsuarioAtual.Login);
                    this.loteCefRepositorio.Recusar(loteCefId, this.userSession.UsuarioAtual.Id);
                    break;
            }
        }

        private void AlterarStatusDossies(int loteCefId)
        {
            var lotes = this.loteRepositorio.ObterPorLoteCefId(loteCefId);
            foreach (var lote in lotes.Where(x => x.Status == LoteStatus.Faturamento || x.Status == LoteStatus.AguardandoControleQualidadeCef))
            {
                try
                {
                    this.gravaLogDoLoteServico.Executar(LogLote.AcaoLotecefAprovadonaQualicef, lote.Id, string.Format("Dossie faz parte do lotecef aprovado [{0}] : status anterior [{1}]", loteCefId, lote.Status.Value));
                }
                catch (Exception)
                {
                    ////faz nada
                }
            }

            this.loteRepositorio.AtualizarTodosParaAprovadoPorLotecef(loteCefId);
        }
    }
}
