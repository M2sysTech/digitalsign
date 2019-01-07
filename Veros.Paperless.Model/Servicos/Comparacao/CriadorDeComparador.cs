namespace Veros.Paperless.Model.Servicos.Comparacao
{
    using System;
    using System.Collections.Generic;
    using Entidades;

    [Serializable]
    public class CriadorDeComparador : ICriadorDeComparador
    {
        private readonly IDictionary<TipoDado, Func<IComparador>> comparadoresTipoDado;
        private readonly IDictionary<TipoCampo, Func<IComparador>> comparadoresTipoCampo;

        public CriadorDeComparador()
        {
            ////this.comparadoresTipoDado = new Dictionary<TipoDado, Func<IComparador>>();
            ////this.comparadoresTipoDado.Add(TipoDado.DateTime, () => new ComparadorDeDatas());
            ////this.comparadoresTipoDado.Add(TipoDado.Numeric, () => new ComparadorDeNumeros());
            ////this.comparadoresTipoDado.Add(TipoDado.Text, () => new ComparadorDeTextos());
            ////this.comparadoresTipoDado.Add(TipoDado.ShortText, () => new ComparadorDeTextoCurto());
            ////this.comparadoresTipoDado.Add(TipoDado.CodigoDeBarras, () => new ComparadorDeCodigoDeBarras());

            ////this.comparadoresTipoCampo = new Dictionary<TipoCampo, Func<IComparador>>();
            ////this.comparadoresTipoCampo.Add(TipoCampo.Nome, () => new ComparadorNome());

            this.comparadoresTipoDado = new Dictionary<TipoDado, Func<IComparador>>();
            this.comparadoresTipoDado.Add(TipoDado.DateTime, () => new ComparadorDeDatas());
            this.comparadoresTipoDado.Add(TipoDado.Numeric, () => new ComparadorDeNumeros());
            this.comparadoresTipoDado.Add(TipoDado.Text, () => new ComparadorDeTextos());
            this.comparadoresTipoDado.Add(TipoDado.ShortText, () => new ComparadorDeTextoCurto());
            this.comparadoresTipoDado.Add(TipoDado.Bool, () => new ComparadorBoleano());
            this.comparadoresTipoDado.Add(TipoDado.PrimeiroEUltimo, () => new ComparadorPrimeiroEUltimoNome());

            this.comparadoresTipoCampo = new Dictionary<TipoCampo, Func<IComparador>>();
            this.comparadoresTipoCampo.Add(TipoCampo.Nome, () => new ComparadorNome());
            this.comparadoresTipoCampo.Add(TipoCampo.NomeMae, () => new ComparadorMae());
            this.comparadoresTipoCampo.Add(TipoCampo.Filiacao, () => new ComparadorFiliacao());
            this.comparadoresTipoCampo.Add(TipoCampo.RegistroRg, () => new ComparadorRegistroRg());
            this.comparadoresTipoCampo.Add(TipoCampo.DataMesAno, () => new ComparadorDeDatasComMesEAno());
            this.comparadoresTipoCampo.Add(TipoCampo.Nenhum, () => new ComparadorDeTextos());
            this.comparadoresTipoCampo.Add(TipoCampo.DataGenerica, () => new ComparadorDeDatas());
        }

        public IComparador Cria(TipoDado tipoDado)
        {
            return this.comparadoresTipoDado[tipoDado]();
        }

        public IComparador Cria(TipoCampo tipoCampo)
        {
            return this.comparadoresTipoCampo[tipoCampo]();
        }

        public IComparador CriaEncontrandoTipoDado(string valorParaComparacao)
        {
            return this.comparadoresTipoDado[this.EncontraTipoDado(valorParaComparacao)]();
        }

        private TipoDado EncontraTipoDado(string valorParaComparacao)
        {
            if (string.IsNullOrEmpty(valorParaComparacao) == false && valorParaComparacao.Length == 8)
            {
                var dataFormatada = valorParaComparacao.FormatarDataIndexacao();

                DateTime conteudoConvertidoDataSemMascara;
                if (DateTime.TryParse(dataFormatada, out conteudoConvertidoDataSemMascara))
                {
                    return TipoDado.DateTime;
                }
            }

            double conteudoConvertidoDouble;
            if (double.TryParse(valorParaComparacao, out conteudoConvertidoDouble))
            {
                return TipoDado.Numeric;
            }

            DateTime conteudoConvertidoData;
            if (DateTime.TryParse(valorParaComparacao, out conteudoConvertidoData))
            {
                return TipoDado.DateTime;
            }

            return TipoDado.Text;
        }
    }
}