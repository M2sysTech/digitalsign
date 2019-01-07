namespace Veros.Paperless.Model.Servicos.DocumentoComProblema
{
    using System;
    using System.Linq;
    using Framework.Servicos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.ViewModel;

    public class MarcarDocumentoComProblema : IMarcarDocumentoComProblema
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IGravaLogDoProcessoServico gravaLogDoProcessoServico;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public MarcarDocumentoComProblema(
            ISessaoDoUsuario userSession,
            IRegraVioladaRepositorio regraVioladaRepositorio, 
            IGravaLogDoProcessoServico gravaLogDoProcessoServico, 
            IDocumentoRepositorio documentoRepositorio, 
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.userSession = userSession;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.gravaLogDoProcessoServico = gravaLogDoProcessoServico;
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(DocumentoComProblemaViewModel viewModel)
        {
            var regraViolada = this.ObterRegraViolada(viewModel);
            regraViolada.Observacao = viewModel.TipoDeProblemaCompleto();
            regraViolada.Pagina = viewModel.Pagina;
            regraViolada.Hora = DateTime.Now;
            regraViolada.Usuario = (Usuario)this.userSession.UsuarioAtual;

            switch (viewModel.RegraId)
            {
                case Regra.CodigoRegraDocumentoComProblemaNaClassificacao:
                    regraViolada.Status = RegraVioladaStatus.Pendente;
                    this.MarcarDocumento(viewModel.DocumentoId, 
                        LogDocumento.AcaoMarcadoComProblemaNaClassificacao, 
                        "Documento marcado com problema na classificação");
                    break;
                case Regra.CodigoRegraQualidadeM2:
                    regraViolada.Status = RegraVioladaStatus.Pendente;
                    this.MarcarDocumento(viewModel.DocumentoId,
                        LogDocumento.AcaoMarcadoComProblemaNaQualidadeM2Ssys, 
                        "Documento marcado com problema na qualidade M2sys");
                    break;
                case Regra.CodigoRegraQualidadeCef:
                    regraViolada.Status = RegraVioladaStatus.Pendente;
                    this.MarcarDocumento(viewModel.DocumentoId,
                        LogDocumento.AcaoMarcadoComProblemaNaQualidadeCef, 
                        "Documento marcado com problema na qualidade CEF");
                    break;
                default:
                    regraViolada.Status = RegraVioladaStatus.Marcada;
                    break;
            }

            this.regraVioladaRepositorio.Salvar(regraViolada);

            this.gravaLogDoProcessoServico.Executar(LogProcesso.AcaoDocumentoMarcadoComProblema, 
                viewModel.ObterProcesso(), 
                string.Format("{0} - MDoc: {1} - Página: {2}", viewModel.TipoProblema, viewModel.DocumentoId, viewModel.Pagina));
        }

        private void MarcarDocumento(int documentoId, string tipoLog, string descricaoLog)
        {
            this.documentoRepositorio.AlterarIndicioDeFraude(documentoId, Documento.MarcaDeProblema);
            this.documentoRepositorio.AlterarStatus(documentoId, DocumentoStatus.AjustePreparacao);

            this.gravaLogDoDocumentoServico.Executar(
                tipoLog, 
                documentoId,
                descricaoLog);
        }

        private RegraViolada ObterRegraViolada(DocumentoComProblemaViewModel viewModel)
        {
            var regrasVioladas = this.regraVioladaRepositorio.ObterRegraEmAbertoPorProcesso(viewModel.ProcessoId, viewModel.RegraId, viewModel.DocumentoId);

            if (regrasVioladas.Any() == false)
            {
                return new RegraViolada
                {
                    Regra = new Regra { Id = viewModel.RegraId },
                    Processo = viewModel.ObterProcesso(),
                    Documento = viewModel.ObterDocumento()
                };
            }

            return regrasVioladas.Last();
        }
    }
}
