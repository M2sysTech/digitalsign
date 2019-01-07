namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;

    [Serializable]
    public class RetornoBanco : EnumerationString<RetornoBanco>
    {
        public static RetornoBanco ErroBanco = new RetornoBanco("ERB", "Erro Banco");
        public static RetornoBanco ErroTerceira = new RetornoBanco("ETE", "Erro Terceira");
        public static RetornoBanco Liberada = new RetornoBanco("LIB", "Liberada");
        public static RetornoBanco Devolvida = new RetornoBanco("DEV", "Devolvida");
        public static RetornoBanco Fraude = new RetornoBanco("FRD", "Fraude");
        public static RetornoBanco Erro = new RetornoBanco("ERR", "Erro");
        
        public RetornoBanco(string value, string displayName)
            : base(value, displayName)
        {
        }

        public static string ObterDescricao(string sigla)
        {
            if (string.IsNullOrEmpty(sigla))
            {
                return string.Empty;
            }

            var retornoBanco = RetornoBanco.FromValue(sigla);

            return retornoBanco != null ? retornoBanco.DisplayName : string.Empty;
        }
    }
}
