namespace Veros.Paperless.Model.Servicos.Batimento.Experimental.BatimentoPorDocumento
{
    using Entidades;

    public interface IBatimentoDocumento
    {
        ResultadoBatimentoDocumento Entre(Documento documento, ImagemReconhecida imagemReconhecida);
    }
}