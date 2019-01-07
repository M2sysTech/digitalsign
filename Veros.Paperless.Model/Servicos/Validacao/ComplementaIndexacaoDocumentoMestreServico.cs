namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class ComplementaIndexacaoDocumentoMestreServico
    {
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly SalvarIndexacaoDocumentoMestre salvarIndexacaoDocumentoMestre;

        public ComplementaIndexacaoDocumentoMestreServico(
            IDocumentoRepositorio documentoRepositorio, 
            SalvarIndexacaoDocumentoMestre salvarIndexacaoDocumentoMestre)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.salvarIndexacaoDocumentoMestre = salvarIndexacaoDocumentoMestre;
        }

        public void Executar(IEnumerable<Documento> documentosMestres, Documento documento)
        {
            foreach (var documentoMestre in documentosMestres)
            {
                this.BateUmDocumentoMestre(documento, documentoMestre);
            }
        }

        private void BateUmDocumentoMestre(Documento documento, Documento documentoMestre)
        {            
            var indexacaoDocumentoComprovacao = documento.Indexacao;

            if (this.ComprovanteCorrespondeAoDocumentoMestre(documento, documentoMestre) == false)
            {
                return;
            }

            foreach (var indiceDoDocumento in indexacaoDocumentoComprovacao)
            {
                this.salvarIndexacaoDocumentoMestre.Executar(documentoMestre, indiceDoDocumento);
            }

            this.SalvarDocumento(documentoMestre);
        }

        private bool ComprovanteCorrespondeAoDocumentoMestre(Documento documento, Documento documentoMestre)
        {
            var cpfDocumento = documento.Indexacao
               .Where(x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCpf);

            var cpfDocumentoMestre = documentoMestre.Indexacao
                .Where(x => x.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCpf);

            if (cpfDocumentoMestre.Any() == false || cpfDocumento.Any() == false)
            {
                return false;
            }

            if (string.IsNullOrEmpty(cpfDocumento.First().SegundoValor))
            {
                return false;
            }

            if (string.IsNullOrEmpty(cpfDocumentoMestre.First().SegundoValor))
            {
                return false;
            }

            return cpfDocumento.First().SegundoValor.Equals(cpfDocumentoMestre.First().SegundoValor);
        }

        private void SalvarDocumento(Documento documentoMestre)
        {
            this.documentoRepositorio.AtualizaStatusDocumento(
                documentoMestre.Id,
                documentoMestre.EstaBatido() ? DocumentoStatus.StatusValidado : DocumentoStatus.StatusParaProvaZero);
        }
    }
}
