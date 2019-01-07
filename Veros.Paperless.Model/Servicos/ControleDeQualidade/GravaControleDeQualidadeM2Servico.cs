namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using System.Linq;
    using Entidades;
    using Framework.Modelo;
    using Repositorios;

    public class GravaControleDeQualidadeM2Servico : IGravaControleDeQualidadeM2Servico
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public GravaControleDeQualidadeM2Servico(
            IProcessoRepositorio processoRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IGravaLogDoProcessoServico gravaLogDoProcessoServico, 
            IDocumentoRepositorio documentoRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(int processoId, string acao, string observacao)
        {
            var processo = this.processoRepositorio.ObterComLote(processoId);

            if (processo.Status != ProcessoStatus.AguardandoControleQualidadeM2 ||
                processo.Lote.Status != LoteStatus.AguardandoControleQualidadeM2)
            {
                throw new RegraDeNegocioException("Processo não está com status aguardando Qualidade M2!");
            }

            var documentos = this.documentoRepositorio.ObterPdfsPorProcesso(processoId);            

            LoteStatus novoStatusLote;

            switch (acao)
            {
                case "M":
                    
                    this.loteRepositorio.SetarMarcaQualidade(processo.Lote.Id, LoteMarcaQualidade.PossuiProblemaQualidade);

                    this.loteRepositorio.PriorizarLote(processo.Lote.Id);

                    novoStatusLote = LoteStatus.AguardandoAjustes;
                    processo.Status = ProcessoStatus.AguardandoAjuste;
                    break;

                case "A":

                    if (documentos.Any(x => x.TipoDocumento.Id == TipoDocumento.CodigoNaoIdentificado))
                    {
                        throw new RegraDeNegocioException("Existem documentos não identificados neste processo. Identifique o(s) documento(s) e tente novamente.");
                    }

                    novoStatusLote = LoteStatus.AguardandoGeracaoTermos;
                    processo.Status = ProcessoStatus.PdfMontado;
                    break;

                default:
                    throw new RegraDeNegocioException("Ação não é válida!");
            }

            processo.UsuarioResponsavel = null;
            processo.HoraInicio = null;
            processo.HoraInicioAjuste = null;
            this.processoRepositorio.Salvar(processo);

            this.loteRepositorio.AlterarStatus(processo.Lote.Id, novoStatusLote);

            this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoControleQualidadeM2, processo, "Controle de Qualidade Realizado - " + acao);
        }
    }
}
