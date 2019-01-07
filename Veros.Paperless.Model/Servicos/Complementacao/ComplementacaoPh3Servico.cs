namespace Veros.Paperless.Model.Servicos.Complementacao
{
    using System;
    using Data;
    using Framework;
    using Repositorios;
    using Storages;
    using Comparacao;

    public class ComplementacaoPh3Servico
    {
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPortalPh3 portalPh3;
        private readonly IConsultaPfStorage consultaPfStorage;

        public ComplementacaoPh3Servico(
            ILoteRepositorio loteRepositorio,
            IUnitOfWork unitOfWork,
            IPortalPh3 portalPh3,
            IConsultaPfStorage consultaPfStorage)
        {
            this.loteRepositorio = loteRepositorio;
            this.unitOfWork = unitOfWork;
            this.portalPh3 = portalPh3;
            this.consultaPfStorage = consultaPfStorage;
        }

        public void Executar()
        {
            var lotes = this.unitOfWork.Obter(() => this.loteRepositorio.ObterPendentesComplementacao());

            foreach (var lote in lotes)
            {
                try
                {
                    var cpf = lote.Identificacao.RemoverDiacritico();

                    var dados = this.portalPh3.ObterDadosPessoais(cpf);
                    this.consultaPfStorage.Adicionar(cpf, dados);
                }
                catch (Exception exception)
                {
                    Log.Application.Error("Não foi possível consultar ph3a do lote " + lote.Id,
                        exception);
                }
                finally
                {
                    this.unitOfWork.Transacionar(() => this.loteRepositorio.AtualizaParaLoteConsultado(lote.Id));
                }
            }
        }
    }
}