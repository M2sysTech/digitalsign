namespace Veros.Paperless.Model.Entidades
{
    public class LogPacote : LogBase
    {
        //// TODO: Verificar código de ação coreto
        public static string AcaoEtiquetaNaRecepcao = "ER";
        public static string AcaoCaixaDevolvida = "DV";
        
        public virtual Pacote Pacote { get; set; }
    }
}