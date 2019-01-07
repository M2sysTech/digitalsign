namespace Veros.Paperless.Model.Servicos.Neurotech
{
    public class RetornoNeurotech
    {
        public RetornoNeurotech(string chave, string valor)
        {
            this.Chave = chave;
            this.Valor = valor;
        }

        public string Chave
        {
            get; 
            set;
        }

        public string Valor
        {
            get; 
            set;
        }
    }
}