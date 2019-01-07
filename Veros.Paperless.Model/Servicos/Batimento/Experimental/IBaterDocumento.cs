namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using Entidades;

    public interface IBaterDocumento
    {
        ResultadoBatimentoDocumento Iniciar(Documento documento, ImagemReconhecida imagemReconhecida);
    }
}