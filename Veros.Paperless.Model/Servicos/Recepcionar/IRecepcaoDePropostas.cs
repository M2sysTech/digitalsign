namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using Veros.Paperless.Model.Entidades;

    public interface IRecepcaoDePropostas
    {
        void Receber(int loteId, ImagemConta imagemConta);
    }
}