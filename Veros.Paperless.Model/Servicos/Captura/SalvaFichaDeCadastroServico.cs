namespace Veros.Paperless.Model.Servicos.Captura
{
    using Entidades;
    using Repositorios;

    public class SalvaFichaDeCadastroServico : ISalvaFichaDeCadastroServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;

        public SalvaFichaDeCadastroServico(
            IIndexacaoRepositorio indexacaoRepositorio,
            ILoteRepositorio loteRepositorio,
            IDocumentoRepositorio documentoRepositorio)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.documentoRepositorio = documentoRepositorio;
        }

        public void Salvar(Documento fichaDeCadastro)
        {
            foreach (var indexacao in fichaDeCadastro.Indexacao)
            {
                if (string.IsNullOrEmpty(indexacao.ObterValor()) == false)
                {
                    this.indexacaoRepositorio.SalvarNaCaptura(indexacao);
                }
            }

            if (fichaDeCadastro.Lote == null)
            {
                fichaDeCadastro.Lote = this.documentoRepositorio.ObterPorId(fichaDeCadastro.Id).Lote;
            }

            ////TODO: Precisa de gravar mais informações? (log, hora, ...)
            this.loteRepositorio.AlterarStatus(fichaDeCadastro.Lote.Id, LoteStatus.CapturaFinalizada);
        }
    }
}
