namespace Veros.Paperless.Model.Servicos.RecepcaoDossieEsperado
{
    using Batimento;
    using Entidades;
    using Framework.Modelo;

    public class DossieEsperadoFabrica
    {        
        public const int ColunaIdentificacao = 0;

        public DossieEsperado Criar(string[] colunas, int caixaId)
        {
            var dossieEsperado = new DossieEsperado()
            {
                MatriculaAgente = this.ObterMatriculaAgente(colunas[2]),
                NumeroContrato = this.ObterNumeroContrato(colunas[2]),
                Hipoteca = this.ObterNumeroHipoteca(colunas[2]),
                NomeDoMutuario = null,
                CodigoDeBarras = colunas[1],
                UfArquivo = colunas[3],
                Situacao = "DC",
                Identificacao = this.ObterCaixa(colunas[0]),
                PacoteId = caixaId
            };

            return dossieEsperado;
        }

        public DossieEsperado Atualizacao(DossieEsperado dossieCriado, DossieEsperado dossieCadastrado)
        {
            dossieCadastrado.MatriculaAgente = dossieCriado.MatriculaAgente;
            dossieCadastrado.NumeroContrato = dossieCriado.NumeroContrato;
            dossieCadastrado.Hipoteca = dossieCriado.Hipoteca;
            dossieCadastrado.NomeDoMutuario = dossieCriado.NomeDoMutuario;
            dossieCadastrado.CodigoDeBarras = dossieCriado.CodigoDeBarras;
            dossieCadastrado.UfArquivo = dossieCriado.UfArquivo;
            dossieCadastrado.Situacao = dossieCriado.Situacao;
            dossieCadastrado.Identificacao = dossieCriado.Identificacao;

            return dossieCadastrado;
        }

        private string ObterCaixa(string conteudo)
        {
            var caixa = conteudo.ObterInteiros();

            if (string.IsNullOrEmpty(caixa))
            {
                throw new RegraDeNegocioException(string.Format("Formato inválido. Coluna 0 #{0}", conteudo)); 
            }

            return caixa.PadLeft(6, '0');
        }

        private string ObterMatriculaAgente(string conteudo)
        {
            var posicaoPonto = conteudo.IndexOf(".", System.StringComparison.Ordinal);

            if (posicaoPonto < 1)
            {
                throw new RegraDeNegocioException(string.Format("Formato inválido. Coluna 2 #{0}", conteudo));
            }

            var matriculaAgente = conteudo.Substring(0, posicaoPonto);            

            return matriculaAgente;
        }

        private string ObterNumeroContrato(string conteudo)
        {
            var posicaoTraco = conteudo.IndexOf("-", System.StringComparison.Ordinal);

            if (posicaoTraco < 7)
            {
                throw new RegraDeNegocioException(string.Format("Formato inválido. Coluna 2 #{0}", conteudo));
            }

            var numeroContrato = conteudo.Substring(6, posicaoTraco - 6);

            return numeroContrato;
        }

        private string ObterNumeroHipoteca(string conteudo)
        {
            var posicaoTraco = conteudo.IndexOf("-", System.StringComparison.Ordinal);

            if (posicaoTraco < 1)
            {
                throw new RegraDeNegocioException(string.Format("Formato inválido. Coluna 2 #{0}", conteudo));
            }

            var numeroHipoteca = conteudo.Substring(posicaoTraco + 1);

            return numeroHipoteca;
        }
    }
}
