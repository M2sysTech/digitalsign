namespace Veros.Paperless.Model.Servicos.Documentos
{
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class GravaIndicioDeFraudeServico : IGravaIndicioDeFraudeServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio;
        private readonly ISessaoDoUsuario userSession;

        public GravaIndicioDeFraudeServico(
            IDocumentoRepositorio documentoRepositorio,
            IEstatisticaAprovacaoRepositorio estatisticaAprovacaoRepositorio,
            ISessaoDoUsuario userSession)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.estatisticaAprovacaoRepositorio = estatisticaAprovacaoRepositorio;
            this.userSession = userSession;
        }

        public void Executar(int documentoId, string indicioDeFraude)
        {
            if (string.IsNullOrEmpty(indicioDeFraude) == false)
            {
                this.IncrementarEstatistica(documentoId);
            }

            this.documentoRepositorio.AlterarIndicioDeFraude(documentoId, indicioDeFraude);
        }

        private void IncrementarEstatistica(int documentoId)
        {
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            if (string.IsNullOrEmpty(documento.IndicioDeFraude))
            {
                this.estatisticaAprovacaoRepositorio.IncrementarFraudesParaHoje(this.userSession.UsuarioAtual.Id);
            }
        }
    }
}