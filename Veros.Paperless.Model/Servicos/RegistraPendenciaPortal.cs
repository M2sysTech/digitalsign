namespace Veros.Paperless.Model.Servicos
{
    using System;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class RegistraPendenciaPortal : IRegistraPendenciaPortal
    {
        private readonly IPendenciaPortalRepositorio pendenciaPortalRepositorio;

        public RegistraPendenciaPortal(IPendenciaPortalRepositorio pendenciaPortalRepositorio)
        {
            this.pendenciaPortalRepositorio = pendenciaPortalRepositorio;
        }

        public PendenciaPortal Incrementar(int solicitacaoId, Exception exception, Operacao operacao)
        {
            var pendenciaRecepcao = this.pendenciaPortalRepositorio
                .ObterPorSolicitacaoId(solicitacaoId);

            if (pendenciaRecepcao == null)
            {
                pendenciaRecepcao = new PendenciaPortal
                {
                    SolicitacaoId = solicitacaoId
                };
            }

            pendenciaRecepcao.Tentativas++;
            pendenciaRecepcao.UltimoErro = exception.Message;
            pendenciaRecepcao.UltimaTentativa = DateTime.Now;
            pendenciaRecepcao.Operacao = operacao;

            this.pendenciaPortalRepositorio.Salvar(pendenciaRecepcao);

            return pendenciaRecepcao;
        }
    }
}