namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using Entidades;
    using Repositorios;

    public class PacoteProcessadoFactory : IPacoteProcessadoFactory
    {
        private readonly IPacoteProcessadoRepositorio pacoteProcessadoRepositorio;
        private readonly IRelogio relogio;

        public PacoteProcessadoFactory(
            IPacoteProcessadoRepositorio pacoteProcessadoRepositorio,
            IRelogio relogio)
        {
            this.pacoteProcessadoRepositorio = pacoteProcessadoRepositorio;
            this.relogio = relogio;
        }

        public PacoteProcessado ObterDoDia()
        {
            var pacoteDoDia = this.pacoteProcessadoRepositorio.ObterPacoteDoDia();

            if (pacoteDoDia == null)
            {
                pacoteDoDia = new PacoteProcessado
                {
                    StatusPacote = StatusPacote.EmProcessamento,
                    ArquivoRecebidoEm = this.relogio.Hoje(),
                    Arquivo = "PrimeiroPacote_" + this.relogio.Agora().ToShortTimeString(),
                    Ativado = "S"
                };
            }

            return pacoteDoDia;
        }
    }
}