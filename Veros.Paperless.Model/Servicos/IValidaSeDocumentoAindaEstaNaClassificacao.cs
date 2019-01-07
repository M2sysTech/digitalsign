namespace Veros.Paperless.Model.Servicos
{
    public interface IValidaSeDocumentoAindaEstaNaClassificacao
    {
        bool Validar(int documentoId);
    }
}