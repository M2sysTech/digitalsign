namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entidades;
    using Data;
    using Framework;
    using Repositorios;

    public class LogBatimentoServico : ILogBatimentoServico
    {
        private readonly ILogBatimentoRepositorio logBatimentoRepositorio;
        private readonly IUnitOfWork unitOfWork;

        public LogBatimentoServico(
            ILogBatimentoRepositorio logBatimentoRepositorio, 
            IUnitOfWork unitOfWork)
        {
            this.logBatimentoRepositorio = logBatimentoRepositorio;
            this.unitOfWork = unitOfWork;
        }

        public void ExecutarValorReconhecido(Indexacao indexacao, ValorReconhecido valorReconhecido)
        {
            new TaskFactory().StartNew(() =>
            {
                try
                {
                    var logBatimento = new LogBatimento
                    {
                        Indexacao = indexacao,
                        CampoEngine = valorReconhecido.CampoTemplate,
                        ValorReconhecido = valorReconhecido.Value,
                    };

                    this.unitOfWork.Transacionar(() => this.logBatimentoRepositorio.Salvar(logBatimento));
                }
                catch (Exception exception)
                {
                    Log.Application.Error(exception);
                }
            });
        }

        public void ExecutarFullText(Indexacao indexacao, List<dynamic> palavras)
        {
            new TaskFactory().StartNew(() =>
            {
                try
                {
                    var palavra = string.Empty;

                    palavra = palavras.Aggregate(palavra,
                        (current, dynamic) => string.Concat(current, ",", dynamic.Texto));

                    var logBatimento = new LogBatimento
                    {
                        Indexacao = indexacao,
                        CampoEngine = "FullText",
                        ValorReconhecido = palavra,                        
                    };

                    this.unitOfWork.Transacionar(() => this.logBatimentoRepositorio.Salvar(logBatimento));
                }
                catch (Exception exception)
                {   
                    Log.Application.Error(exception);
                }
            });
        }
    }
}