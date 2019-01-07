namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    public interface IGravaLotesCefDoContextoServico
    {
        void Executar();

        void ExecutarRecusadosAguardandoNovaAmostra();
    }
}
