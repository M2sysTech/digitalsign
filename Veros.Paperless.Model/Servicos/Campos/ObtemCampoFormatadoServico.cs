namespace Veros.Paperless.Model.Servicos.Campos
{
    using System.Globalization;
    using System.Linq;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class ObtemCampoFormatadoServico : IObtemCampoFormatadoServico
    {
        private readonly IDominioCampoRepositorio dominioCampoRepositorio;

        public ObtemCampoFormatadoServico(IDominioCampoRepositorio dominioCampoRepositorio)
        {
            this.dominioCampoRepositorio = dominioCampoRepositorio;
        }

        public string Obter(Documento documento, int campoId, bool ignorarFormatacao = false)
        {
            var indexacao = documento.Indexacao.FirstOrDefault(x => x.Campo.Id == campoId);

            if (indexacao != null)
            {
                if (ignorarFormatacao)
                {
                    return indexacao.ObterValor();
                }

                return this.Obter(indexacao.Campo, indexacao.ObterValor());
            }

            return string.Empty;
        }

        public string Obter(Campo campo, string valor)
        {
            if (valor == null || valor.Equals(string.Empty))
            {
                return string.Empty;
            }

            if (valor == "#")
            {
                return "INEXISTENTE";
            }

            if (valor == "?")
            {
                return "ILEGÍVEL";
            }

            var tipoValidacao = this.ObterValorDeComplemento(campo.MascaraComplemento, "Validacao");

            if (tipoValidacao.Equals("CheckOne"))
            {
                return this.ObterValorDeDominio(valor, campo);
            }

            if (tipoValidacao.Equals("Monetario"))
            {
                return this.ObterFormatacaoMonetaria(valor);
            }

            return this.ObterValorFormatado(valor, campo.Mascara).Trim();
        }

        private string ObterValorDeDominio(string valor, Campo campo)
        {
            var tipoDominio = this.ObterValorDeComplemento(campo.MascaraComplemento, "TabelaDinamica");

            if (tipoDominio.Equals(string.Empty))
            {
                return valor;
            }

            var dominio = this.dominioCampoRepositorio.ObterPorCodigoEChave(tipoDominio, valor);

            if (dominio != null)
            {
                return dominio.Descricao ?? valor;
            }

            return valor;
        }

        private string ObterFormatacaoMonetaria(string valor)
        {
            if (string.IsNullOrEmpty(valor) || valor.Length < 3)
            {
                return valor;
            }

            valor = valor.Substring(0, valor.Length - 2) + "," + valor.Substring(valor.Length - 2);

            double monetario;

            if (double.TryParse(valor, out monetario))
            {
                return monetario.ToString("C", CultureInfo.CurrentCulture);
            }

            return valor;
        }

        private string ObterValorFormatado(string valor, string mascara)
        {
            if (mascara.Equals(string.Empty) || mascara.IndexOf("@{") == 0)
            {
                return valor;
            }

            if (mascara.Length <= valor.Length)
            {
                return valor;
            }

            var valorFormatado = string.Empty;
            var posicaoValor = 0;

            for (var posicaoMascara = 0; posicaoMascara < mascara.Length; posicaoMascara++)
            {
                if (mascara[posicaoMascara].Equals('#'))
                {
                    if (posicaoValor >= valor.Length)
                    {
                        return valor;
                    }

                    valorFormatado += valor[posicaoValor];
                    posicaoValor++;
                }
                else
                {
                    valorFormatado += mascara[posicaoMascara];
                }
            }

            return valorFormatado;
        }

        private string ObterValorDeComplemento(string texto, string chave)
        {
            texto = ";" + texto;

            int inicioChave = texto.IndexOf(";" + chave, System.StringComparison.Ordinal);

            if (inicioChave < 0)
            {
                return string.Empty;
            }

            int inicioValor = texto.IndexOf("=", inicioChave, System.StringComparison.Ordinal);

            if (inicioValor < 0)
            {
                return string.Empty;
            }

            int fimValor = texto.IndexOf(";", inicioValor, System.StringComparison.Ordinal);

            return texto.Substring(inicioValor + 1, fimValor - inicioValor - 1);
        }
    }
}