namespace Veros.Paperless.Model.Servicos.Ajustes
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public class AplicarAjusteDeExclusaoDePagina : IAplicarAjusteDeExclusaoDePagina
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;

        public AplicarAjusteDeExclusaoDePagina(
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
        }

        public void Executar(Pagina pagina)
        {
            var documentoAntigo = pagina.Documento.Id;

            var documentacaoGeralInicial = this.documentoRepositorio.ObterDocumentosDoLotePorTipo(
                pagina.Lote.Id,
                TipoDocumento.CodigoDocumentoGeral).FirstOrDefault(x => x.Versao == "0");

            if (documentacaoGeralInicial == null)
            {
                var mensagemErro = string.Format(
                    "Falha ao tentar excluir Pagina original #{0} do documento #{1} - Documento original (docGeral) nao encontradofoi setada como excluida.",
                    pagina.Id, documentoAntigo);

                Log.Application.Error(mensagemErro);

                throw new Exception(mensagemErro);
            }

            pagina.Documento = documentacaoGeralInicial;
            pagina.Status = PaginaStatus.StatusExcluida;
            this.paginaRepositorio.Salvar(pagina);

            Log.Application.InfoFormat("Pagina original #{0} do documento #{1} foi setada como excluida.", pagina.Id, documentoAntigo);
        }
    }
}