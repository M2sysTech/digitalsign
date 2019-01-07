namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using System.Linq;
    using BatimentoPorDocumento;
    using Veros.Framework;
    using Veros.Paperless.Model.Entidades;

    public class BatimentoDocumento<TBatimentoIndexacaoMapeada> : 
        IBatimentoDocumento where TBatimentoIndexacaoMapeada : IBatimentoIndexacaoMapeada
    {
        private readonly IIndexacaoMapeada indexacaoMapeada;

        public BatimentoDocumento(IIndexacaoMapeada indexacaoMapeada)
        {
            this.indexacaoMapeada = indexacaoMapeada;
        }

        public ResultadoBatimentoDocumento Entre(Documento documento, ImagemReconhecida imagemReconhecida)
        {
            var indexacoesMapeadas = this.indexacaoMapeada.ComValoresReconhecidos(
                documento.Indexacao.ToList(), 
                imagemReconhecida.ValoresReconhecidos);

            var resultadoBatimento = new ResultadoBatimentoDocumento();

            var batimento = IoC.Current.Resolve(typeof(TBatimentoIndexacaoMapeada));
                
            var batimentoResidencia = ((IBatimentoIndexacaoMapeada)batimento)
                .ProximaTentativa(IoC.Current.Resolve<ComprovanteResidenciaConsultaCepBatimento>());

            ////Caso haja a necessidade de incluir mais classes de batimento pode-se hierarquizar como abaixo:
            //// var batimentoRg = ((IBatimentoIndexacaoMapeada)batimento)
            ////    .ProximaTentativa(IoC.Current.Resolve<RgBatimento>()); 
            //// 
            //// var batimentoRgReceita = ((IBatimentoIndexacaoMapeada)batimento)
            ////    .ProximaTentativa(IoC.Current.Resolve<RgConsultaReceitaBatimento>()); 

            foreach (var mapeada in indexacoesMapeadas)
            {
                var batido = ((IBatimentoIndexacaoMapeada)batimento).EstaBatido(mapeada);

                var campoBatido = new CampoBatido
                {
                    Indexacao = mapeada.Indexacao,
                    Batido = batido,
                    TipoBatimento = TipoBatimento.Template
                };

                resultadoBatimento.AdicionarOuEditar(campoBatido);
            }
            
            return resultadoBatimento;
        }
    }
}