namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using System.Collections.Generic;
    using BatimentoPorCampo;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework;

    public class BatimentoCampo<T> : IBatimentoCampo where T : IBatimentoFullTextCampo
    {
        public CampoBatido Entre(
            Indexacao indexacao,
            IList<PalavraReconhecida> palavrasReconhecidas)
        {
            var batimento = IoC.Current.Resolve(typeof(T));

            this.ConfigurarProximaTentativa(
                indexacao,
                (IBatimentoFullTextCampo)batimento);

            var batido = ((IBatimentoFullTextCampo)batimento)
                .EstaBatido(indexacao, palavrasReconhecidas);

            var campoBatido = new CampoBatido
            {
                Indexacao = indexacao,
                Batido = batido,
                TipoBatimento = TipoBatimento.FullText
            };
            
            return campoBatido;
        }

        private void ConfigurarProximaTentativa(
            Indexacao indexacao,
            IBatimentoFullTextCampo batimento)
        {
            switch (indexacao.Campo.ReferenciaArquivo)
            {
                case Campo.ReferenciaDeArquivoNomeMaeCliente:
                    batimento.ProximaTentativa(IoC.Current.Resolve<NomeMaeCnhBatimento>());
                    break;

                case Campo.ReferenciaDeArquivoNomePaiCliente:
                    batimento.ProximaTentativa(IoC.Current.Resolve<NomePaiCnhBatimento>());
                    break;

                default:
                    batimento.ProximaTentativa(null);
                    break;
            }
        }
    }
}