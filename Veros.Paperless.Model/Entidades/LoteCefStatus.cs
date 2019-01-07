namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;

    [Serializable]
    public class LoteCefStatus : EnumerationString<LoteCefStatus>
    {
        public static LoteCefStatus Aberto = new LoteCefStatus("AB", "Aberto");

        public static LoteCefStatus Fechado = new LoteCefStatus("FE", "Fechado");

        public static LoteCefStatus AprovadoNaQualidade = new LoteCefStatus("AP", "Aprovado na Qualidade");

        public static LoteCefStatus ReprovadoNaQualidade = new LoteCefStatus("RP", "Reprovado na Qualidade");

        public static LoteCefStatus ReprovadoAguardandoNovaAmostra = new LoteCefStatus("RN", "Reprovado Aguardando Nova Amostra");

        public static LoteCefStatus Excluido = new LoteCefStatus("*", "Excluído");

        public LoteCefStatus(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
