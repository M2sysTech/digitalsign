namespace Veros.Paperless.Model.Servicos
{
    using Entidades;

    public interface IExpurgaProcessoServico
    {
        void Executar(Processo processo);
    }
}