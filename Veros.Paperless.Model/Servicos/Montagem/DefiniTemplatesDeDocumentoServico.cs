namespace Veros.Paperless.Model.Servicos.Montagem
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class DefiniTemplatesDeDocumentoServico : IDefiniTemplatesDeDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ITemplateRepositorio templateRepositorio;
        private readonly IObtemTemplatesOrdenadosServico obtemTemplatesOrdenadosServico;
        private readonly IValorReconhecidoRepositorio valorReconhecidoRepositorio;

        public DefiniTemplatesDeDocumentoServico(
            IDocumentoRepositorio documentoRepositorio,
            ITemplateRepositorio templateRepositorio,
            IObtemTemplatesOrdenadosServico obtemTemplatesOrdenadosServico,
            IValorReconhecidoRepositorio valorReconhecidoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.templateRepositorio = templateRepositorio;
            this.obtemTemplatesOrdenadosServico = obtemTemplatesOrdenadosServico;
            this.valorReconhecidoRepositorio = valorReconhecidoRepositorio;
        }

        public void Executar(Documento documento)
        {
            Log.Application.Debug("Processando documento " + documento.Id);

            if (documento.Status != DocumentoStatus.AguardandoMontagem)
            {
                Log.Application.Debug("Documento não está aguardando montagem " + documento.Id);
                return;
            }

            Log.Application.Debug("Procurando templates do tipo de documento " + documento.TipoDocumento.Id);
            var templatesDoDocumento = this.templateRepositorio.ObterPorTipoDeDocumento(documento.TipoDocumento.Id);

            Log.Application.Debug("Procurando valores reconhecidos do documento " + documento.Id);
            var valoresReconhecidos = this.valorReconhecidoRepositorio.ObtemPorDocumento(documento.Id);

            documento.Templates = this.obtemTemplatesOrdenadosServico.Obter(
                valoresReconhecidos,
                templatesDoDocumento);

            if (string.IsNullOrEmpty(documento.Templates) == false)
            {
                this.documentoRepositorio.ConcluirMontagemDocumento(
                    documento.Id,
                    documento.Templates);
            }
        }
    }
}