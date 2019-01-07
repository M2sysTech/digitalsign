namespace Veros.Paperless.Model.Servicos.Aprovacao
{
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class RegistraTextoPadraoParaDocumentosComFraudeServico : IRegistraTextoPadraoParaDocumentosComFraudeServico
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public RegistraTextoPadraoParaDocumentosComFraudeServico(
            IRegraVioladaRepositorio regraVioladaRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Executar(Processo processo)
        {
            var regrasDeFraude = this.regraVioladaRepositorio.ObterRegrasDeFraude(processo.Id);

            if (regrasDeFraude == null)
            {
                return;
            }

            foreach (var regraViolada in regrasDeFraude.Where(x => x.Documento != null && string.IsNullOrEmpty(x.Documento.IndicioDeFraude)))
            {
                this.documentoRepositorio.AlterarIndicioDeFraude(regraViolada.Documento.Id, "Documentacao com indicio de fraude");
            }
        }
    }
}