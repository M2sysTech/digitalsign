namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using Entidades;
    using Framework.Modelo;
    using Framework.Servicos;
    using Repositorios;

    public class GravaControleDeQualidadeCefServico : IGravaControleDeQualidadeCefServico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;
        private readonly ILoteCefRepositorio loteCefRepositorio;
        private readonly ISessaoDoUsuario sessaoDoUsuario;
        private readonly IGravaLogGenericoServico gravaLogGenericoServico;

        public GravaControleDeQualidadeCefServico(
            IProcessoRepositorio processoRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDoProcessoServico gravaLogDoProcessoServico, 
            ILoteCefRepositorio loteCefRepositorio,
            ISessaoDoUsuario sessaoDoUsuario, 
            IGravaLogGenericoServico gravaLogGenericoServico)
        {
            this.processoRepositorio = processoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
            this.loteCefRepositorio = loteCefRepositorio;
            this.sessaoDoUsuario = sessaoDoUsuario;
            this.gravaLogGenericoServico = gravaLogGenericoServico;
        }

        public void Executar(int processoId, string acao, string observacao)
        {
            var processo = this.processoRepositorio.ObterPorId(processoId);
            this.Executar(processo, acao, observacao);
        }

        public void Executar(Processo processo, string acao, string observacao)
        {
            LoteStatus novoStatusLote;
            processo.Status = ProcessoStatus.ControleQualidadeCefFinalizado;

            string descricaoAcao;

            switch (acao)
            {
                case "M":
                    descricaoAcao = "Marcado";
                    novoStatusLote = LoteStatus.AguardandoAjustes;
                    processo.Status = ProcessoStatus.AguardandoAjuste;
                    break;

                case "R":
                    descricaoAcao = "Reprovado"; 
                    novoStatusLote = LoteStatus.AguardandoAjustes;
                    processo.Status = ProcessoStatus.AguardandoAjuste;

                    this.loteCefRepositorio.Recusar(processo.Lote.LoteCef.Id, this.sessaoDoUsuario.UsuarioAtual.Id);

                    this.gravaLogGenericoServico.Executar(
                        LogGenerico.AcaoReprovarLoteCef, 
                        processo.Lote.LoteCef.Id, 
                        observacao, 
                        "QualidadeCEF",
                        this.sessaoDoUsuario.UsuarioAtual.Login);

                    break;

                case "A":
                    descricaoAcao = "Aprovado";
                    novoStatusLote = LoteStatus.ControleQualidadeCefRealizado;
                    break;

                default:
                    throw new RegraDeNegocioException("Ação não é válida!");
            }

            processo.UsuarioResponsavel = null;
            processo.HoraInicio = null;
            processo.Marca = Processo.MarcaPassouPelaQualidadeCef;
            this.processoRepositorio.Salvar(processo);

            this.loteRepositorio.GravarResultadoQualidadeCef(processo.Lote.Id, novoStatusLote, acao);

            this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoControleQualidadeCef, processo, "Controle de Qualidade CEF Realizado - " + descricaoAcao);
        }
    }
}
