namespace Veros.Paperless.Model.Storages
{
    using Veros.Paperless.Model.Servicos.Complementacao;

    public interface IConsultaVertrosStorage
    {
        VertrosStatus Obter(string cpf);

        void Adicionar(string cpf, VertrosStatus vertrosStatus);

        void Apagar(string cpf);
    }
}