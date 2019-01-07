namespace Veros.Paperless.Model.Servicos.Complementacao
{
    using System;
    using Veros.Framework;

    [Serializable]
    public class VertrosStatus : EnumerationString<VertrosStatus>
    {
        public static VertrosStatus CpfOk = new VertrosStatus("200", "Cpf Ok");
        public static VertrosStatus CpfNok = new VertrosStatus("400", "Cpf Não Ok");
        public static VertrosStatus AutenticacaoInvalida = new VertrosStatus("502", "Usuario e Senha Incorretos");

        public static VertrosStatus ApiKeyInvalida = new VertrosStatus("503", "API_KEY Invalida");
        public static VertrosStatus CpfInvalido = new VertrosStatus("504", "Cpf Invalido");
        public static VertrosStatus AcessoNegadoALicenca = new VertrosStatus("505", "Access Denied - MaxQuery Reached/Invalid license");

        public static VertrosStatus AtaqueTerceiros = new VertrosStatus("506", "Access Denied – Man in the Middle detected");
        public static VertrosStatus TimeoutCriptografia = new VertrosStatus("507", "Cryptographic services timeout");
        public static VertrosStatus ServidorOcupado = new VertrosStatus("508", "Server Busy");
        
        public static VertrosStatus BancoIndisponivel = new VertrosStatus("509", "Access Denied – Database not accessible");
        public static VertrosStatus ErroInterno = new VertrosStatus("510", "Internal Server Error");
        public static VertrosStatus NaoIdentificado = new VertrosStatus("outro", "Nao Identificado");

        public VertrosStatus(string value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
