namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using Veros.Framework;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class PacoteFactory : IPacoteFactory
    {
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IAgenciaRepositorio agenciaRepositorio;

        public PacoteFactory(
            IPacoteRepositorio pacoteRepositorio, 
            IAgenciaRepositorio agenciaRepositorio)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.agenciaRepositorio = agenciaRepositorio;
        }

        public Pacote Criar()
        {
            ////TODO: Verificar se não seria melhor coloar uma agência fixa (exemplo: Banco: Blue Box, Agencia: Site)
            var agencia = this.agenciaRepositorio.ObterPorNumeroAgenciaBancoId("1735", 1);

            var pacote = this.pacoteRepositorio.ObterPacoteAbertoNaEstacao("00000" + agencia.Numero);

            if (pacote != null)
            {
                return pacote;
            }

            Log.Application.Debug("Criando pacote");

            pacote = new Pacote
            {
                Agencia = agencia.Id,
                Batido = "N",
                Bdu = "0000000",
                DataMovimento = DateTime.Now,
                FromHost = "N",
                HoraInicio = DateTime.Now,
                Estacao = agencia.Numero,
                Status = Pacote.Aberto
            };

            return pacote;
        }
    }
}
