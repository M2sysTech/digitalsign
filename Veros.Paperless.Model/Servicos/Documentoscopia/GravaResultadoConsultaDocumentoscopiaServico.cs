namespace Veros.Paperless.Model.Servicos.Documentoscopia
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;
    using Model.Documentoscopia;
    using Repositorios;

    public class GravaResultadoConsultaDocumentoscopiaServico
    {
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public GravaResultadoConsultaDocumentoscopiaServico(
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public void Execute(Processo processo, ResultadoConsultaDocumentoscopia resultado)
        {
            if (resultado == null)
            {
                Log.Application.Error("Resultado da consulta foi nulo - processo #" + processo.Id);
                return;
            }

            var documentoFicha = processo.Documentos.First(x => x.TipoDocumento.Id == TipoDocumento.CodigoFichaDeCadastro);

            if (documentoFicha == null)
            {
                Log.Application.Error("Nao foi possível localizar o documento ficha no processo #" + processo.Id);
                return;
            }
                
            this.GravarValor(documentoFicha, Campo.CampoIndicadorFormatoDataEmissao, resultado.ScoreDataExpedicao);
            this.GravarValor(documentoFicha, Campo.CampoIndicadorNumeroRegistroCnh, resultado.ScoreCnh);
            this.GravarValor(documentoFicha, Campo.CampoIndicadorPopularidadeGeral, resultado.PopGeral);
            this.GravarValor(documentoFicha, Campo.CampoIndicadorPopularidadeDataExpedicao, resultado.PopDataExpedicao);
            this.GravarValor(documentoFicha, Campo.CampoIndicadorScoreCpfDataExpedicao, resultado.ScoreCpfDataExpedicao);
            this.GravarValor(documentoFicha, Campo.CampoIndicadorScoreCpfUf, resultado.ScoreCpfUf);
            ////this.GravarValor(documentoFicha, Campo.CampoIndicadorGraficaImpressao, resultado.ScoreGraficaImpressao);
            ////this.GravarValor(documentoFicha, Campo.CampoIndicadorAssinaturaDelegado, resultado.ScoreAssinatura);
        }

        private void GravarValor(Documento ficha, int campoId, string valor)
        {
            var indexacao = this.ObterIndexacao(ficha, campoId);

            indexacao.SegundoValor = valor;
            indexacao.UsuarioSegundoValor = -2;
            indexacao.DataSegundaDigitacao = DateTime.Now;

            this.indexacaoRepositorio.Salvar(indexacao);
        }

        private Indexacao ObterIndexacao(Documento ficha, int campoId)
        {
            var indexacao = ficha.Indexacao.FirstOrDefault(x => x.Campo.Id == campoId);
            
            if (indexacao == null)
            {
                return new Indexacao
                {
                    Campo = new Campo { Id = campoId },
                    Documento = ficha
                };    
            }

            return indexacao;
        }
    }
}
