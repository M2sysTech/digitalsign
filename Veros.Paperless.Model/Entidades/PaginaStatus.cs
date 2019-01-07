namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;

    [Serializable]
    public class PaginaStatus : EnumerationString<PaginaStatus>
    {
        public static readonly PaginaStatus StatusTransmissaoOk = new PaginaStatus("35", "Transmissão Ok");
        public static readonly PaginaStatus StatusAguardandoRetransmissaoImagem = new PaginaStatus("3X", "Aguardando Retransmissão de Imagem");
        
        public static readonly PaginaStatus StatusParaReconhecimento = new PaginaStatus("55", "Para Reconhecimento");
        public static readonly PaginaStatus StatusReconhecimentoExecutado = new PaginaStatus("5A", "Reconhecimento Executado");

        public static readonly PaginaStatus StatusParaReconhecimentoPosAjuste = new PaginaStatus("5B", "Para Reconhecimento Pos Ajuste");

        public static readonly PaginaStatus StatusAguardandoValidacao = new PaginaStatus("95", "Aguardando Validação");
        public static readonly PaginaStatus StatusValidado = new PaginaStatus("9A", "Validado");

        public static readonly PaginaStatus Finalizada = new PaginaStatus("G0", "Finalizada");

        public static readonly PaginaStatus StatusNaoReconhecida = new PaginaStatus("00", "Não ReconhecimentoConcluido");
        public static readonly PaginaStatus StatusExcluida = new PaginaStatus("*", "Excluída");

        public PaginaStatus(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
