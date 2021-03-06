﻿namespace Veros.Paperless.Model.Servicos.Classificacao
{
    using Entidades;
    using Framework.Modelo;
    using Repositorios;

    public class ReclassificaDocumentoSeparadoServico : IReclassificaDocumentoSeparadoServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IGravaLogDoDocumentoServico gravaLogDoDocumentoServico;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;

        public ReclassificaDocumentoSeparadoServico(
            IDocumentoRepositorio documentoRepositorio,
            IGravaLogDoDocumentoServico gravaLogDoDocumentoServico, 
            ITipoDocumentoRepositorio tipoDocumentoRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.gravaLogDoDocumentoServico = gravaLogDoDocumentoServico;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
        }
        
        public void Executar(int documentoId, int tipoDocumentoId)
        {
            var documento = this.documentoRepositorio.ObterPorId(documentoId);

            this.Executar(documento, tipoDocumentoId);
        }

        public void Executar(Documento documento, int tipoDocumentoId)
        {
            if (documento == null || documento.Status == DocumentoStatus.Excluido)
            {
                throw new RegraDeNegocioException("O documento não encontrado");
            }

            if (tipoDocumentoId < 1)
            {
                tipoDocumentoId = documento.TipoDocumento.Id;
            }

            var tipoDocumentoSelecionado = this.tipoDocumentoRepositorio.ObterPorId(tipoDocumentoId);

            if (tipoDocumentoId == TipoDocumento.CodigoNaoIdentificado || tipoDocumentoId == TipoDocumento.CodigoDocumentoGeral)
            {
                throw new RegraDeNegocioException("Tipo de documento não é válido");
            }
            
            if (tipoDocumentoId == documento.TipoDocumento.Id)
            {
                this.documentoRepositorio.AlterarStatus(documento.Id, DocumentoStatus.IdentificacaoConcluida);
                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoClassificacaoAprovadaNaFormalistica,
                    documento.Id,
                    "Documento aprovado na formalística. Tipo: " + documento.TipoDocumento.Description);

                return;
            }

            if (tipoDocumentoId != TipoDocumento.CodigoAguardandoNovoTipo &&
                tipoDocumentoId != TipoDocumento.CodigoNaoIdentificado)
            {
                documento.Status = DocumentoStatus.IdentificacaoConcluida;
            }

            var tipoDocumentoCorreto = tipoDocumentoSelecionado.GrupoId == 0 ? 
                tipoDocumentoSelecionado :
                this.tipoDocumentoRepositorio.ObterPorId(tipoDocumentoSelecionado.GrupoId);

            this.Classificar(tipoDocumentoCorreto, tipoDocumentoSelecionado, documento);
        }

        private void Classificar(TipoDocumento tipoDocumentoCorreto, TipoDocumento tipoDocumento, Documento documento)
        {
            documento.TipoDocumento = tipoDocumentoCorreto;
            documento.Reclassificado = true;

            this.documentoRepositorio.AtualizarAposClassificacao(documento.Status, documento.TipoDocumento, documento.Id);

            this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoDocumentoReclassificado,
                    documento.Id,
                    "Documento reclassificado para " + documento.TipoDocumento.Description);

            if (tipoDocumento != tipoDocumentoCorreto)
            {
                this.gravaLogDoDocumentoServico.Executar(LogDocumento.AcaoDocumentoGenerico,
                    documento.Id,
                    string.Format("Tipo Documento ajustado de [{0}] para [{1}]", tipoDocumento.Id, tipoDocumentoCorreto.Id));
            }
        }
    }
}
