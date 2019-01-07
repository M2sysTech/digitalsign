namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using System.Linq;
    using Campos;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class ObtemDetalhamentoDoDocumentoServico : IObtemDetalhamentoDoDocumentoServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        private readonly IObtemCampoFormatadoServico obtemCampoFormatadoServico;
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IObtemDetalhamentoDoDocumentoCampoServico obtemDetalhamentoDoDocumentoCampoServico;

        public ObtemDetalhamentoDoDocumentoServico(
            IIndexacaoRepositorio indexacaoRepositorio,
            IObtemCampoFormatadoServico obtemCampoFormatadoServico,
            ICampoRepositorio campoRepositorio,
            IDocumentoRepositorio documentoRepositorio, 
            IObtemDetalhamentoDoDocumentoCampoServico obtemDetalhamentoDoDocumentoCampoServico)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
            this.obtemCampoFormatadoServico = obtemCampoFormatadoServico;
            this.campoRepositorio = campoRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.obtemDetalhamentoDoDocumentoCampoServico = obtemDetalhamentoDoDocumentoCampoServico;
        }

        public DetalhamentoDoDocumento Obter(int documentoId)
        {
            var detalhamento = new DetalhamentoDoDocumento();
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            var indexacoes = this.indexacaoRepositorio.ObterTodosPorDocumentoComOsCampos(new Documento { Id = documentoId });
            
            detalhamento.Grupos = this.ObterGrupos(indexacoes, documento.TipoDocumento);

            return detalhamento;
        }

        private IList<DetalhamentoDoDocumentoGrupo> ObterGrupos(IList<Indexacao> indexacoes, TipoDocumento tipoDocumento)
        {
            var campos = this.campoRepositorio.ObterCamposComGrupo();

            var gruposDetalhados = campos.Select(x => x.Grupo).Distinct().Select(grupoCampo => new DetalhamentoDoDocumentoGrupo
            {
                GrupoCampo = grupoCampo,
                Campos = this.ObterCamposGrupo(indexacoes, grupoCampo, campos, tipoDocumento)
            })
            .OrderBy(x => x.GrupoCampo.Ordem)
            .ToList();

            return gruposDetalhados;
        }

        private IList<DetalhamentoDoDocumentoCampo> ObterCamposGrupo(
            IList<Indexacao> indexacoes, 
            GrupoCampo grupoCampo,
            IEnumerable<Campo> todosCampos, 
            TipoDocumento tipoDocumento)
        {
            var detalhamentos = new List<DetalhamentoDoDocumentoCampo>();

            var campos = todosCampos.Where(x => x.Grupo == grupoCampo && x.TipoDocumento == tipoDocumento).OrderBy(x => x.OrdemNoGrupo);

            foreach (var campo in campos)
            {
                detalhamentos.AddRange(this.DetalhamentosDoCampo(indexacoes, campo));
            }

            return detalhamentos;
        }

        private IEnumerable<DetalhamentoDoDocumentoCampo> DetalhamentosDoCampo(IEnumerable<Indexacao> indexacoes, Campo campo)
        {
            var indexacoesDoCampo = indexacoes.Where(x => x.Campo == campo).ToList();

            if (indexacoesDoCampo.Any())
            {
                return indexacoesDoCampo.Select(indexacao => 
                    this.obtemDetalhamentoDoDocumentoCampoServico.Obter(indexacao)).ToList();
            }

            return new List<DetalhamentoDoDocumentoCampo> 
                {
                    new DetalhamentoDoDocumentoCampo { Campo = campo }
                };
        }
    }
}