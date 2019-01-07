namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using System;
    using Entidades;
    using Repositorios;

    public class CriaPacoteProcessado : ICriaPacoteProcessado
    {
        private readonly IPacoteProcessadoRepositorio pacoteProcessadoRepositorio;

        public CriaPacoteProcessado(IPacoteProcessadoRepositorio pacoteProcessadoRepositorio)
        {
            this.pacoteProcessadoRepositorio = pacoteProcessadoRepositorio;
        }

        public PacoteProcessado Executar(string nomeDoPacote, DateTime? horaUltimoArquivo)
        {
            var pacoteProcessado = new PacoteProcessado
            {
                Arquivo = nomeDoPacote,
                ArquivoRecebidoEm = DateTime.Now,
                UltimoArquivoRecebido = horaUltimoArquivo.GetValueOrDefault(),
                StatusPacote = StatusPacote.Pendente
            };

            this.pacoteProcessadoRepositorio.Salvar(pacoteProcessado);

            return pacoteProcessado;
        }
    }
}