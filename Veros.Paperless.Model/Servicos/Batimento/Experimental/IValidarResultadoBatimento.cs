namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using Entidades;

    public interface IValidarResultadoBatimento
    {
        ResultadoBatimentoDocumento ValidarNumeroDocumentoIdentificacao(ResultadoBatimentoDocumento resultadoBatimento, Documento documento);
    }
}