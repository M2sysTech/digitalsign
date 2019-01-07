namespace Veros.Paperless.Model.Storages
{
    using Ph3;
    using Servicos.Complementacao;

    public interface IConsultaPfStorage
    {
        ConsultaPf Obter(string cpf);

        void Adicionar(string cpf, ConsultaPf consultas);

        void Apagar(string cpf);
    }
}