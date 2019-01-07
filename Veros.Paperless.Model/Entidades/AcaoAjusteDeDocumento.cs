namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;
    
    [Serializable]
    public class AcaoAjusteDeDocumento : EnumerationString<AcaoAjusteDeDocumento>
    {
        public static AcaoAjusteDeDocumento UsarOriginal = new AcaoAjusteDeDocumento("OR", "Usar Imagem Original");
        public static AcaoAjusteDeDocumento ExcluirPagina = new AcaoAjusteDeDocumento("EX", "Excluir Página");
        public static AcaoAjusteDeDocumento GirarHorario = new AcaoAjusteDeDocumento("G1", "Girar - Horário");
        public static AcaoAjusteDeDocumento GirarAntiHorario = new AcaoAjusteDeDocumento("G2", "Girar - Anti-Horário");
        public static AcaoAjusteDeDocumento Girar180 = new AcaoAjusteDeDocumento("G3", "Girar - 180°");
        public static AcaoAjusteDeDocumento Reclassificar = new AcaoAjusteDeDocumento("RE", "Reclassificar");
        
        public AcaoAjusteDeDocumento(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
