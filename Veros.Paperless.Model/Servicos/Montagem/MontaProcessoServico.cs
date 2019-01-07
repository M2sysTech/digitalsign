namespace Veros.Paperless.Model.Servicos.Montagem
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class MontaProcessoServico : IMontaProcessoServico
    {
        private readonly IDefiniTemplatesDeDocumentoServico definiTemplatesDeDocumentoServico;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public MontaProcessoServico(
            IDefiniTemplatesDeDocumentoServico definiTemplatesDeDocumentoServico, 
            IProcessoRepositorio processoRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.definiTemplatesDeDocumentoServico = definiTemplatesDeDocumentoServico;
            this.processoRepositorio = processoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Montar(Processo processo)
        {
            Log.Application.Info("Montando processo #" + processo.Id);

            foreach (var documento in processo.Documentos)
            {
                this.documentoRepositorio.ConcluirMontagemDocumento(
                    documento.Id,
                    documento.Templates);
            }

            this.processoRepositorio.AlterarStatus(processo.Id, ProcessoStatus.Montado);
        }
    }
}