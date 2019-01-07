namespace Veros.Paperless.Model.Servicos
{
    using Veros.Paperless.Model.Entidades;

    public interface IGravaLogDoProcessoServico
    {
        void Executar(string acaoLogProcesso,
            Processo processo,
            string observacao);
    }
}