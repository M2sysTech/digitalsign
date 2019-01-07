namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class AcaoVideoStatus : EnumerationString<AcaoVideoStatus>
    {
        public static AcaoVideoStatus OlharParaCima = new AcaoVideoStatus("1", "Olhar Para Cima");
        public static AcaoVideoStatus OlharParaBaixo = new AcaoVideoStatus("2", "Olhar Para Baixo");
        public static AcaoVideoStatus OlharParaEsquerda = new AcaoVideoStatus("3", "Olhar Para Esquerda");
        public static AcaoVideoStatus OlharParaDireita = new AcaoVideoStatus("4", "Olhar Para Direita");
        public static AcaoVideoStatus Sorria = new AcaoVideoStatus("5", "Sorria");
        public static AcaoVideoStatus FiqueSerio = new AcaoVideoStatus("6", "Fique Serio");

        public AcaoVideoStatus(string value, string counterName) : 
            base(value, counterName)
        {
        }
    }
}