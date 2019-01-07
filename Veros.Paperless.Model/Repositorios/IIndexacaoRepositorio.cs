namespace Veros.Paperless.Model.Repositorios
{
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Framework.Modelo;

    public interface IIndexacaoRepositorio : IRepositorio<Indexacao>
    {
        Indexacao ObterPorCampoDeUmDocumento(int campoId, Documento documento);

        IList<Indexacao> ObterTodosPorDocumentoComOsCampos(Documento documento);

        void AlterarPrimeiroValor(int indexacaoId, string valor);

        IList<Indexacao> ObterValidadosPorDataDeProducao(DateTime dataProducao);

        IList<Indexacao> ObterPendentesDeDigitacaoPorDocumento(Documento documento);

        bool ExisteValorPreenchidoParaCampoPorProcesso(int campoId, int processoId);

        bool ExistePorCampoEProcesso(int tipoCampoId, int processoId);

        IList<Indexacao> ObterPorCampoEProcesso(int tipoCampoId, int processoId);
        
        void AlterarCampo(int indexacaoId, int campoId);

        void AlterarValorFinal(int indexacaoId, string valorNovo);

        IList<Indexacao> ObterTodosPorPeriodo(DateTime dataInicial, DateTime dataFinal);
        
        IList<Indexacao> ObterTodosValidadosPorPeriodo(DateTime dataInicial, DateTime dataFinal);
        
        IList<Indexacao> ObterPorCampoPorPeriodo(Campo campo, DateTime dataInicial, DateTime dataFinal);
        
        IList<Indexacao> ObterPorTipoCampoDeUmDocumento(int documentoId, TipoCampo tipoCampo);

        IList<Indexacao> ObterPorReferenciaDeArquivo(int documentoId, string referenciaDeArquivo);

        IList<Indexacao> ObterComMapeamentoPorDocumento(Documento documento);

        void AlterarCampoPorDocumentoECampo(int documentoId, int campoIdOriginal, int campoIdAlterarPara);
        
        void LimparValor1E2(int indexacaoId);

        IList<Indexacao> ObterPorReferenciaArquivoDeUmDocumento(int loteId, string campoRefArquivo);

        void SalvarValorFinalBatidoComOcr(int indexacaoId);

        void SalvarNaCaptura(Indexacao indexacao);

        IList<Indexacao> ObterPorProcesso(int processoId);
    }
}
