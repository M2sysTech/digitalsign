namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using System.IO;
    using Entidades;
    using Framework;
    using Repositorios;

    public class PropostaAberturaContaServico : IPropostaAberturaContaServico
    {
        private readonly IDocumentoFabrica documentoFabrica;
        private readonly IIndexacaoFabrica indexacaoFabrica;
        private readonly IPaginaFabrica paginaFabrica;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IPostaArquivoFileTransferServico postaArquivoFileTransferServico;

        public PropostaAberturaContaServico(
            IDocumentoFabrica documentoFabrica,
            IIndexacaoFabrica indexacaoFabrica,
            IPaginaFabrica paginaFabrica,
            IDocumentoRepositorio documentoRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio,
            IPaginaRepositorio paginaRepositorio,
            IPostaArquivoFileTransferServico postaArquivoFileTransferServico)
        {
            this.documentoFabrica = documentoFabrica;
            this.indexacaoFabrica = indexacaoFabrica;
            this.paginaFabrica = paginaFabrica;
            this.documentoRepositorio = documentoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.postaArquivoFileTransferServico = postaArquivoFileTransferServico;
        }
        
        public Documento CriarPacVirtual(Processo processo, string cpf)
        {
            var fichaDeCadastro = new TipoDocumento { Id = TipoDocumento.CodigoFichaDeCadastro };

            var pac = this.documentoFabrica.Criar(processo, fichaDeCadastro, cpf);
            pac.Virtual = true;

            this.documentoRepositorio.Salvar(pac);

            var indexacao = this.AdicionarDataRecebimentoPac(pac);
            this.indexacaoRepositorio.Salvar(indexacao);

            var fichaVirtual = Path.Combine(Aplicacao.Caminho, "fichapadrao.jpg");
            var pagina = this.paginaFabrica.Criar(pac, 0, fichaVirtual);

            this.paginaRepositorio.Salvar(pagina);

            this.postaArquivoFileTransferServico.PostarPagina(pagina, fichaVirtual);

            return pac;
        }
        
        private Indexacao AdicionarDataRecebimentoPac(Documento pac)
        {
            var campoId = Campo.CampoDataRecebimentoPac;
            var indexacao = this.indexacaoFabrica.Criar(pac, campoId, DateTime.Now.ToString("ddMMyyyy"));
            pac.AdicionaIndexacao(indexacao);

            return indexacao;
        }
    }
}