namespace Veros.Paperless.Model.Servicos
{
    public interface IValidaSeDocumentoAindaEstaNaIdentificacaoManual
    {
        bool Validar(int documentoId);
    }
}