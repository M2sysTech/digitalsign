namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using System;
    using Framework;

    public class ExecutorDeExpressoes : IExecutorDeExpressoes
    {
        private int regraId;

        public string ExecutarFormula(int regraId, string expressao)
        {
            this.regraId = regraId;

            if (string.IsNullOrEmpty(expressao))
            {
                return string.Empty;
            }

            var expressaoFormatada = expressao
                .Replace("#", "0")
                .Replace("?", "0")
                .Replace("!", "0");

            var resultado = this.ExecutaExpressao(expressaoFormatada);

            return this.ObterParteInteira(resultado);
        }

        public bool ExecutarBooleano(int regraId, string expressao)
        {
            this.regraId = regraId;

            if (string.IsNullOrEmpty(expressao))
            {
                return false;
            }

            return Convert.ToBoolean(this.ExecutaExpressao(expressao));
        }

        private string ExecutaExpressao(string expressao)
        {
            try
            {
                var expressaoFormatada = expressao.ToLower().Replace("  ", " ");

                var table = new System.Data.DataTable();

                return table.Compute(expressaoFormatada, string.Empty).ToString();
            } 
            catch (Exception exception)
            {
                Log.Application.Error("Erro ao processar regra [" + this.regraId + "]. Expressao: " + expressao, exception);
                throw;
            }
        }

        private string ObterParteInteira(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return valor;
            }

            return valor.Replace('.', ',').Split(',')[0];
        }
    }
}
