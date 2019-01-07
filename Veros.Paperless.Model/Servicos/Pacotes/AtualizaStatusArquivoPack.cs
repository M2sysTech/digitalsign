namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using Entidades;
    using Repositorios;

    public class AtualizaStatusArquivoPack : IAtualizaStatusArquivoPack
    {
        private readonly IArquivoPackRepositorio arquivoPackRepositorio;

        public AtualizaStatusArquivoPack(IArquivoPackRepositorio arquivoPackRepositorio)
        {
            this.arquivoPackRepositorio = arquivoPackRepositorio;
        }

        public void Executar(int arquivoPackId, ArquivoPackStatus arquivoPackStatus)
        {
            this.arquivoPackRepositorio.AtualizaStatus(arquivoPackId, arquivoPackStatus);
        }
    }
}