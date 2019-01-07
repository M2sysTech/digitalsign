namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface ITitularDocumentoMestreComCertidaoCasamento
    {
        Indexacao Complementar(Documento documentoMestre, Indexacao indexacaoDocumentoMestre);
    }
}