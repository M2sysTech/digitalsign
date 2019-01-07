namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;
    using System.Collections.Generic;

    public interface IConversorFormatoImgServico
    {
        void ConverterProcessoParaJpg(Processo processo, int tamanhoMinimoBytes);

        List<string> ConverterImagemParaJpg(string imagem, int tamanhoMinimoBytes);

        List<string> ConversaoPdf(string caminho, int dpiOut = 150, string pathSaida = "");
    }
}
