namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using Veros.Framework;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class CriaPacoteProcessadoServico : ICriaPacoteProcessadoServico
    {
        private readonly IPacoteProcessadoRepositorio pacoteProcessadoRepositorio;

        public CriaPacoteProcessadoServico(IPacoteProcessadoRepositorio pacoteProcessadoRepositorio)
        {
            this.pacoteProcessadoRepositorio = pacoteProcessadoRepositorio;
        }

        public PacoteProcessado Criar()
        {
            var pacoteProcessado = this.pacoteProcessadoRepositorio.ObterPacoteDoDia();

            if (pacoteProcessado != null)
            {
                Log.Application.DebugFormat("Pacote processado [{0}] já existia na base.", pacoteProcessado.Id);

                return pacoteProcessado;
            }

            Log.Application.Debug("Criando novo pacote processado");

            pacoteProcessado = new PacoteProcessado()
            {
                Arquivo = "site",
                ArquivoImportadoEm = DateTime.Now,
                ArquivoRecebidoEm = DateTime.Now,
                ContaRecepcionadas = 0,
                StatusPacote = StatusPacote.EmProcessamento
            };

            return pacoteProcessado;
        }
    }
}
