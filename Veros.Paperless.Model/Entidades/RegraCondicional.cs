namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Linq;
    using Framework;
    using Framework.Modelo;

    public class RegraCondicional : Entidade
    {
        public RegraCondicional()
        {
            this.OperadorMatematico = OperadorMatematico.Nenhum;
        }

        public virtual Regra Regra
        {
            get;
            set;
        }

        public virtual Binario Binario
        {
            get;
            set;
        }

        public virtual TipoDocumento TipoDocumento
        {
            get;
            set;
        }

        public virtual ConectivoDeComparacao Conectivo
        {
            get;
            set;
        }

        public virtual Campo Campo
        {
            get;
            set;
        }

        public virtual string ValorFixo
        {
            get;
            set;
        }

        public virtual string Coluna
        {
            get;
            set;
        }

        public virtual OperadorMatematico OperadorMatematico
        {
            get;
            set;
        }

        public virtual string FatorMatematico
        {
            get;
            set;
        }

        public virtual Campo CampoParaComparar
        {
            get;
            set;
        }

        public virtual string ColunaParaComparar
        {
            get;
            set;
        }

        public virtual string Funcao
        {
            get;
            set;
        }

        public virtual string FuncaoComparar
        {
            get;
            set;
        }

        public virtual int Ordem
        {
            get;
            set;
        }

        public virtual string ObterValor(Processo processo)
        {
            return ObterValor(processo, this.Campo, this.Coluna, this.Funcao);
        }

        public virtual string ObterValorParaComparar(Processo processo)
        {
            if (string.IsNullOrEmpty(this.ValorFixo) == false)
            {
                return this.AplicarFuncao(this.ValorFixo, this.FuncaoComparar);
            }

            return ObterValor(processo, this.CampoParaComparar, this.ColunaParaComparar, this.FuncaoComparar);
        }

        public virtual bool DeveAplicarFatorMatematico()
        {
            return string.IsNullOrEmpty(this.FatorMatematico) == false ||
                OperadorMatematico == OperadorMatematico.Nenhum;
        }

        public virtual Indexacao ObterIndexacaoParaComparar(Processo processo)
        {
            if (string.IsNullOrEmpty(this.ValorFixo) == false)
            {
                return null;
            }

            var indexacao = processo
                .Documentos
                .SelectMany(documento => documento.Indexacao.Where(x => x.Campo == this.CampoParaComparar))
                .FirstOrDefault();

            return indexacao;
        }

        private string ObterValor(Processo processo, Campo campo, string coluna, string funcao)
        {
            var indexacao = processo
                .Documentos
                .SelectMany(documento => documento.Indexacao.Where(x => x.Campo == campo))
                .FirstOrDefault();

            if (indexacao == null)
            {
                return string.Empty;
            }
             
            if (string.IsNullOrEmpty(coluna))
            {
                coluna = "naoConfigurado";
            }

            var valor = indexacao.ObterValor(coluna);

            return this.AplicarFuncao(valor, funcao);
        }

        private string AplicarFuncao(string valor, string funcao)
        {
            var separadorFuncao = -1;
            var complementoFuncao = string.Empty;

            if (string.IsNullOrEmpty(funcao) || string.IsNullOrEmpty(valor) || valor.Length < 3)
            {
                return valor;
            }

            if (valor == "HOJE")
            {
                valor = string.Format("{0}{1}{2}",
                    DateTime.Today.ToString("dd"),
                    DateTime.Today.ToString("MM"),
                    DateTime.Today.ToString("yyyy"));
            }

            separadorFuncao = funcao.IndexOf(':');
            if (separadorFuncao > 0)
            {
                complementoFuncao = funcao.Substring(separadorFuncao + 1);
                funcao = funcao.Substring(0, separadorFuncao);
            }

            switch (funcao)
            {
                case "DIA":
                    return valor.Substring(0, 2);

                case "MES":
                    return valor.Substring(2, 2);

                case "ANO":
                    return valor.Substring(4);

                case "HOJE":
                    return DateTime.Today.ToString("ddMMyyyy");

                case "SUBSTR":
                    var vetComplemento = complementoFuncao.Split(',');
                    if (vetComplemento.Length == 2)
                    {
                        var inicio = vetComplemento[0].ToInt();
                        var fim = vetComplemento[1].ToInt();

                        return valor.Substring(inicio, valor.Length > fim ? fim : valor.Length);
                    }

                    break;
            }

            return valor;
        }
    }
}
