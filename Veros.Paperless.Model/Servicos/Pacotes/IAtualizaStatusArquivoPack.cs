namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using Entidades;

    public interface IAtualizaStatusArquivoPack
    {
        void Executar(int arquivoPackId, ArquivoPackStatus arquivoPackStatus);
    }
}