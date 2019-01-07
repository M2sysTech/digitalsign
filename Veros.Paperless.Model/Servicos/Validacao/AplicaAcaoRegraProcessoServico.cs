namespace Veros.Paperless.Model.Servicos.Validacao
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;

    public class AplicaAcaoRegraProcessoServico
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IRegraAcaoRepositorio regraAcaoRepositorio;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;

        public AplicaAcaoRegraProcessoServico(
            IRegraVioladaRepositorio regraVioladaRepositorio,
            IRegraAcaoRepositorio regraAcaoRepositorio,
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.regraAcaoRepositorio = regraAcaoRepositorio;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public void Execute(Processo processo)
        {   
            var regrasVioladas = this.regraVioladaRepositorio.ObterRegrasVioladasParaValidacaoPorProcesso(processo.Id);
           
            foreach (var regraViolada in regrasVioladas)
            {
                var regraAcao = this.regraAcaoRepositorio.ObterAcaoPorRegra(regraViolada.Regra.Id);
                
                if (regraAcao.TipoOrigem == TipoOrigem.ResultadoDeFormula)
                {
                    continue;
                }

                foreach (var documento in processo.Documentos)
                {
                    ////TODO: angelo. tá aqui!
                    ////var consultaNeorotech = new NeurotechClient("192.168.10.15");
                    ////var resultado = consultaNeorotech.ExecutarComParametros(documento.id);

                    var indexacaoCampoDestino = documento.Indexacao.FirstOrDefault(x => x.Campo == regraAcao.Destino);

                    if (indexacaoCampoDestino == null)
                    {
                        continue;
                    }

                    if (regraAcao.TipoOrigem == TipoOrigem.ValorFixo)
                    {
                        indexacaoCampoDestino.AtribuirValor(regraAcao.ColunaDestino, regraAcao.ValorFixo);
                        indexacaoCampoDestino.OcrComplementou = true;
                    }
                    else
                    {
                        var indexacaoCampoOrigem = documento.Indexacao.FirstOrDefault(x => x.Campo == regraAcao.Origem);

                        if (indexacaoCampoOrigem != null)
                        {
                            indexacaoCampoDestino.AtribuirValor(
                                regraAcao.ColunaDestino, 
                                indexacaoCampoOrigem.ObterValor(regraAcao.ColunaOrigem));
                            indexacaoCampoDestino.ValorUtilizadoParaValorFinal = indexacaoCampoOrigem.
                                ObterValorUtilizado(regraAcao.ColunaOrigem);

                            indexacaoCampoDestino.OcrComplementou = true;
                        }
                    }

                    this.indexacaoRepositorio.Salvar(indexacaoCampoDestino);
                }
            }            
        }
    }
}
