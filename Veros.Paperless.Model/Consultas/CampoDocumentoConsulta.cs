namespace Veros.Paperless.Model.Consultas
{
    public class CampoDocumentoConsulta
    {
        public CampoDocumentoConsulta(string campoNome)
        {
            this.CampoNome = campoNome;
        }

        public CampoDocumentoConsulta(string campoNome, string valor1)
        {
            this.CampoNome = campoNome;
            this.Valor1 = valor1;
        }

        public string CampoNome { get; set; }

        public string Valor1 { get; set; }
    }
}
