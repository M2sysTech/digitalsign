namespace Veros.Paperless.Model.Servicos.Suporte
{
    public interface IRetornaTriagemServico
    {
        void Executar(int documentoId, string motivo);
    }
}
