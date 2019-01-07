namespace Veros.Paperless.Model.Servicos.Identificacao
{
    using System.Collections.Generic;
    using System.Linq;
    using Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class ObtemTiposBloqueadosParaIdentificacaoServico : IObtemTiposBloqueadosParaIdentificacaoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IConfiguracaoDeTipoDeProcessoRepositorio configuracaoDeTipoDeProcessoRepositorio;

        public ObtemTiposBloqueadosParaIdentificacaoServico(
            IDocumentoRepositorio documentoRepositorio, 
            ITipoDocumentoRepositorio tipoDocumentoRepositorio, 
            IConfiguracaoDeTipoDeProcessoRepositorio configuracaoDeTipoDeProcessoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.configuracaoDeTipoDeProcessoRepositorio = configuracaoDeTipoDeProcessoRepositorio;
        }

        public IList<int> Obter(int documentoId)
        {
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            if (documento == null)
            {
                return null;
            }

            var documentosDoParticipante = this.ObterDocumentosDoParticipante(documento);

            var tiposBloqueados = this.ObterTiposDoParticipante(documentosDoParticipante);

            if (documento.TipoDocumentoOriginal.IsDi)
            {
                tiposBloqueados = this.AdiconarTodosOsTiposNaoDi(tiposBloqueados, documentosDoParticipante);
            }

            if (documento.TipoDocumentoOriginal.IsDi == false)
            {
                if (this.BloquearDi(documentosDoParticipante))
                {
                    tiposBloqueados = this.BloquearTodosOsDis(tiposBloqueados);
                }
            }

            if (documento.TipoDocumentoOriginal.Id == TipoDocumento.CodigoDocumentoGeral)
            {
                tiposBloqueados = this.AdicionarTiposObrigatorios(documento, tiposBloqueados);
            }

            return tiposBloqueados.Select(x => x.Id).Distinct().ToList();
        }

        private List<TipoDocumento> AdicionarTiposObrigatorios(Documento documento, List<TipoDocumento> tiposBloqueados)
        {
            var tiposNaoObrigatorios = this.configuracaoDeTipoDeProcessoRepositorio.ObterTiposNaoObrigatorios(documento.Processo.Id);

            tiposBloqueados.AddRange(tiposNaoObrigatorios);
            return tiposBloqueados;
        }

        private List<TipoDocumento> BloquearTodosOsDis(List<TipoDocumento> tiposBloqueados)
        {
            var todosOsTipos = this.tipoDocumentoRepositorio.ObterTodos();

            tiposBloqueados.AddRange(todosOsTipos.Where(x => x.IsDi));
            return tiposBloqueados;
        }

        private List<TipoDocumento> AdiconarTodosOsTiposNaoDi(List<TipoDocumento> tiposDoParticipante, IEnumerable<Documento> documentosDoParticipante)
        {
            var todosOsTipos = this.tipoDocumentoRepositorio.ObterTodos();

            if (this.BloquearDi(documentosDoParticipante))
            {
                tiposDoParticipante.AddRange(todosOsTipos.Where(x => x.Id != TipoDocumento.CodigoDocumentoGeral));
                return tiposDoParticipante;
            }

            return todosOsTipos.Where(x => x.IsDi == false).ToList();
        }

        private IEnumerable<Documento> ObterDocumentosDoParticipante(Documento documento)
        {
            var documentosDoParticipante = this.documentoRepositorio.ObterDocumentosDoProcessoComCpf(documento.Processo.Id, documento.ObterSegundoValor("CPFBAS"));

            if (documentosDoParticipante == null)
            {
                return null;
            }

            return documentosDoParticipante.ToList();
        }

        private List<TipoDocumento> ObterTiposDoParticipante(IEnumerable<Documento> documentos)
        {
            if (documentos == null)
            {
                return null;
            }

            return documentos.Where(x => x.TipoDocumento.Id != TipoDocumento.CodigoDocumentoGeral).Select(x => x.TipoDocumento).ToList();
        }

        private bool BloquearDi(IEnumerable<Documento> documentosDoParticipante)
        {
            var quantidadeDeDocumentosMestre = documentosDoParticipante.Count(x => x.TipoDocumento.TipoDocumentoEhMestre);
            var quantidadeDeDIs = documentosDoParticipante.Count(x => x.TipoDocumento.IsDi);

            return quantidadeDeDocumentosMestre <= quantidadeDeDIs;
        }
    }
}