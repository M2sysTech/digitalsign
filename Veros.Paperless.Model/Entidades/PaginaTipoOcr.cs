namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;

    [Serializable]
    public class PaginaTipoOcr : EnumerationString<PaginaTipoOcr>
    {
        public static readonly PaginaTipoOcr TipoConteudo = new PaginaTipoOcr("C", "Conteudo");
        public static readonly PaginaTipoOcr TipoBranco = new PaginaTipoOcr("B", "Pagina em branco");
        public static readonly PaginaTipoOcr TipoSeparadorSoftek = new PaginaTipoOcr("S", "Separador Softek");
        public static readonly PaginaTipoOcr TipoSeparadorBarcode = new PaginaTipoOcr("E", "Separador Barcode Csharp");
        public static readonly PaginaTipoOcr TipoSeparadorCarimbo = new PaginaTipoOcr("M", "Separador Carimbo");
        public static readonly PaginaTipoOcr TipoSeparadorContraparte = new PaginaTipoOcr("X", "Contraparte Separador");

        public PaginaTipoOcr(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
