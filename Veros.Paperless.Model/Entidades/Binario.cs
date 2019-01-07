namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class Binario : EnumerationInt<Binario>
    {
        public static readonly Binario B0 = new Binario(0, "Condição fixa");
        public static readonly Binario B1 = new Binario(1, "Se algum [Tipo de Documento] existe");
        public static readonly Binario B2 = new Binario(2, "Se algum [Tipo de Documento] NÃO existe");
        public static readonly Binario B4 = new Binario(4, "Se a regra [outra regra] foi tratada e Aprovada");
        public static readonly Binario B8 = new Binario(8, "Se a regra [outra regra] foi Marcada");
        public static readonly Binario B16 = new Binario(16, "Se o campo [campo] foi preenchido");
        public static readonly Binario B32 = new Binario(32, "Se o campo [campo] NÃO foi preenchido");
        public static readonly Binario B64 = new Binario(64, "Se a comparação [entre dois campos] for atendida");
        public static readonly Binario B128 = new Binario(128, "Somatório de valores de campos do processo");
        public static readonly Binario B256 = new Binario(256, "Quantidade de documentos de determinado tipo no processo");

        public Binario(int value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
