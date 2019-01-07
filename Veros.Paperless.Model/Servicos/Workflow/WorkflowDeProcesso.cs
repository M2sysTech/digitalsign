namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class WorkflowDeProcesso
    {
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IFasesDeProcesso fasesDeProcesso;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;

        public WorkflowDeProcesso(
            IProcessoRepositorio processoRepositorio, 
            IFasesDeProcesso fasesDeProcesso, 
            IDocumentoRepositorio documentoRepositorio, 
            IIndexacaoRepositorio indexacaoRepositorio, 
            ILoteRepositorio loteRepositorio)
        {
            this.processoRepositorio = processoRepositorio;
            this.fasesDeProcesso = fasesDeProcesso;
            this.documentoRepositorio = documentoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.loteRepositorio = loteRepositorio;
        }

        public void Processar(Processo processo, ConfiguracaoDeFases configuracaoDeFases)
        {
            var fases = this.fasesDeProcesso.Obter();

            foreach (var fase in fases)
            {
                fase.Processar(processo, configuracaoDeFases);
            }

            this.SalvarProcesso(processo);
        }

        private void SalvarProcesso(Processo processo)
        {
            this.processoRepositorio.Salvar(processo);

            this.loteRepositorio.Salvar(processo.Lote);

            foreach (var documento in processo.Documentos)
            {
                this.documentoRepositorio.Salvar(documento);

                foreach (var indexacao in documento.Indexacao)
                {
                    this.indexacaoRepositorio.Salvar(indexacao);
                }
            }
        }
    }
}