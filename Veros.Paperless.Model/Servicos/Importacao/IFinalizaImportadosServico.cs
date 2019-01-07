namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Entidades;

    public interface IFinalizaImportadosServico
    {
        void Confirmar(Coleta coleta);

        void Cancelar(Coleta coleta);
    }
}
