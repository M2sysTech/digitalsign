namespace Veros.Paperless.Model.Entidades
{
    public class TipoDocumentoDominioOriginal
    {
        public const string Rne = "40";
        public const string Funcional = "41";
        public const string Cnh = "42";
        public const string Conselho = "43";
        public const string Militar = "44";
        public const string Passaport = "45";
        public const string Rg = "46";
        public const string Ctps = "47";
        public const string Ric = "48";
        public const string Outros = "49";

        public static TipoDocumento ObterTipoDocumento(string valor)
        {
            switch (valor)
            {
                case Cnh:
                    return new TipoDocumento { Id = TipoDocumento.CodigoCnh };
                case Rne:
                    return new TipoDocumento { Id = TipoDocumento.CodigoCie };
                case Militar:
                    return new TipoDocumento { Id = TipoDocumento.CodigoCim };
                case Passaport:
                    return new TipoDocumento { Id = TipoDocumento.CodigoPassaporte };
                case Rg:
                    return new TipoDocumento { Id = TipoDocumento.CodigoRg };
                case Ctps:
                    return new TipoDocumento { Id = TipoDocumento.CodigoCtps };
                default:
                    return new TipoDocumento { Id = TipoDocumento.CodigoRg };
            }
        }
    }
}