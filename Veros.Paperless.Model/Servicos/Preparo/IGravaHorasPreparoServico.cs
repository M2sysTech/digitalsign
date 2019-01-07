namespace Veros.Paperless.Model.Servicos.Preparo
{
    public interface IGravaHorasPreparoServico
    {
        void Executar(string acao, string barcode, string matricula1, string matricula2, string matricula3, string matricula4, string horaInicio, string horaFim);
    }
}
