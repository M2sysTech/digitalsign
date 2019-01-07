namespace Veros.Paperless.Model.Servicos.Validacao
{
    using Veros.Paperless.Model.Entidades;

    public interface ICorrecaoDeNomeServico
    {
        void Execute(Processo processo);
    }
}