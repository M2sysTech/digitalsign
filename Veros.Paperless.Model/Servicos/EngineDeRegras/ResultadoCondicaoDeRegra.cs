namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    public class ResultadoCondicaoDeRegra
    {
        public ResultadoCondicaoDeRegra(string resultado)
        {
            this.Resultado = resultado;
        }

        public ResultadoCondicaoDeRegra(double resultado)
        {
            this.Resultado = resultado.ToString();
        }

        public ResultadoCondicaoDeRegra(bool resultado)
        {
            this.Resultado = resultado.ToString().ToLower();
        }

        public string Resultado { get; set; }

        public bool FoiAtendida()
        {
            return this.Resultado == "true";
        }

        public bool NaoFoiAtendida()
        {
            return this.Resultado == "false";
        }
    }
}
