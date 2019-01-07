namespace Veros.Paperless.Model.Servicos.IdentificacaoManual
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class ReclassificaDocumentoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;

        public ReclassificaDocumentoServico(
            IDocumentoRepositorio documentoRepositorio, 
            IPaginaRepositorio paginaRepositorio, 
            ILoteRepositorio loteRepositorio,
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.paginaRepositorio = paginaRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
        }

        public void Executar(int documentoId, IList<string> paginasId, IList<string> tiposId, IList<string> grupos)
        {
            var documento = this.documentoRepositorio.ObterComPaginas(documentoId);

            if (documento == null || documento.Status == DocumentoStatus.Excluido)
            {
                throw new RegraDeNegocioException("O documento foi classificado");
            }
            
            var ultimoGrupo = -1;
            var ordemDePagina = 1;
            var novoDocumento = new Documento();
            var documentosNovos = new List<Documento>();

            for (var i = 0; i < paginasId.Count; i++)
            {
                var grupo = grupos[i].ToInt();
                var tipoDocumentoId = tiposId[i].ToInt();

                if (grupo != ultimoGrupo)
                {
                    novoDocumento = this.CriarDocumento(documento, tipoDocumentoId);
                    documentosNovos.Add(novoDocumento);
                    ultimoGrupo = grupo;
                    ordemDePagina = 1;
                }

                var pagina = documento.Paginas.FirstOrDefault(x => x.Id == paginasId[i].ToInt());
                pagina.Ordem = ordemDePagina;
                pagina.Documento = novoDocumento;
                novoDocumento.Paginas.Add(pagina);

                ordemDePagina++;
            }

            this.SalvarNovosDocumentos(documentosNovos);
            this.loteRepositorio.AlterarStatus(documento.Lote.Id, LoteStatus.Montado);
            this.ExcluirDocumentoAtual(documento);
        }

        private void ExcluirDocumentoAtual(Documento documento)
        {
            documento.Status = DocumentoStatus.Excluido;
            this.documentoRepositorio.Salvar(documento);

            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoExcluidoNaClassificacao,
                    documento.Id,
                    "Documento excluído na classificação.");
        }

        private void SalvarNovosDocumentos(IEnumerable<Documento> documentosNovos)
        {
            foreach (var documento in documentosNovos)
            {
                this.documentoRepositorio.Salvar(documento);

                foreach (var pagina in documento.Paginas)
                {
                    this.paginaRepositorio.Salvar(pagina);
                }

                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoCriadoNaClassificacao, 
                    documento.Id, 
                    "Documento incluído na classificação.");
            }
        }

        private Documento CriarDocumento(Documento documento, int tipoDocumentoId)
        {
            var codigo = DateTime.Now.ToString("fffssmmhhdd");

            return Documento.Novo(
                    new TipoDocumento { Id = tipoDocumentoId },
                    codigo,
                    documento.Lote,
                    documento.Processo);
        }
    }
}
