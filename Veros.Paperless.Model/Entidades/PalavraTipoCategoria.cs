namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;

    [Serializable]
    public class PalavraTipoCategoria : EnumerationString<PalavraTipoCategoria>
    {
        public static PalavraTipoCategoria Requerida = new PalavraTipoCategoria("RQ", "Requerida");
        public static PalavraTipoCategoria Proibida = new PalavraTipoCategoria("PB", "Proibida");
        public static PalavraTipoCategoria Opcional = new PalavraTipoCategoria("OP", "Opcional");
        public static PalavraTipoCategoria Falha = new PalavraTipoCategoria("**", "Falhou");

        public PalavraTipoCategoria(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
