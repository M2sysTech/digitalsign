namespace Veros.Paperless.Model.Servicos.Captura
{
    public class VerificaSeEhFichaDeCadastroServico : IVerificaSeEhFichaDeCadastroServico
    {
        public bool EhFichaDeCadastro(string imagem)
        {
            ////var x = new Veros.Image.Barcodes.BarcodeRecognizer();
            ////var resultado = x.Recognize(imagem);

            ////return resultado == "FICHA01";
            return false;
        }
    }
}
