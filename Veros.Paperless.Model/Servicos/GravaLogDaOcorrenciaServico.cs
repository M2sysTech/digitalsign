namespace Veros.Paperless.Model.Servicos
{
    using System;
    using Veros.Paperless.Model.Entidades;
    using Veros.Framework.Servicos;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogDaOcorrenciaServico : IGravaLogDaOcorrenciaServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IOcorrenciaLogRepositorio ocorrenciaLogRepositorio;

        public GravaLogDaOcorrenciaServico(
            ISessaoDoUsuario userSession,
            IOcorrenciaLogRepositorio ocorrenciaLogRepositorio)
        {
            this.userSession = userSession;
            this.ocorrenciaLogRepositorio = ocorrenciaLogRepositorio;
        }

        public void Executar(
            string acao,
            int ocorrenciaId,
            string observacao)
        {
            this.Executar(acao, new Ocorrencia { Id = ocorrenciaId }, observacao);
        }

        public void Executar(
            string acao,
            Ocorrencia ocorrencia,
            string observacao)
        {
            var logOcorrencia = new OcorrenciaLog
            {
                DataRegistro = DateTime.Now,
                Acao = acao,
                Ocorrencia = ocorrencia,
                Observacao = observacao,
                UsuarioRegistro = (Usuario)this.userSession.UsuarioAtual
            };

            this.ocorrenciaLogRepositorio.Salvar(logOcorrencia);
        }
    }
}