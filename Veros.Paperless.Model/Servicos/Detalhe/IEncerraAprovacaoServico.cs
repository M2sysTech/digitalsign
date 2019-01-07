namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using Veros.Paperless.Model.Entidades;

    public interface IEncerraAprovacaoServico
    {
        void Executar(Processo processo);
    }
}