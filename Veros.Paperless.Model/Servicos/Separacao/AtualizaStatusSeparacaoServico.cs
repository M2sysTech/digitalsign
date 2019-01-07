namespace Veros.Paperless.Model.Servicos.Separacao
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;
    using Data;
    using Entidades;
    using Framework;
    using Repositorios;

    public class AtualizaStatusSeparacaoServico : IAtualizaStatusSeparacaoServico
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico; 

        public AtualizaStatusSeparacaoServico(UnitOfWork unitOfWork, 
            IProcessoRepositorio processoRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico)
        {
            this.unitOfWork = unitOfWork;
            this.processoRepositorio = processoRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
        }

        public void RegistrarLogSubfaseSeparacao(int loteId, int processoId, string subfase)
        {
            new TaskFactory().StartNew(() =>
            {
                try
                {
                    this.unitOfWork.Transacionar(() =>
                    {
                        //// atualiza proc_starttime
                        this.processoRepositorio.AtualizarHoraInicio(processoId);

                        //// grava log no lote 
                        var observacao   = string.Empty;
                        if (LogLote.AcaoSeparacaoSubfaseDownload == subfase)
                        {
                            observacao = string.Format("Fim da subfase de Download na Separacao [{0}]", this.GetLocalIpAddress());
                        }

                        if (LogLote.AcaoSeparacaoSubfaseBrancos == subfase)
                        {
                            observacao = string.Format("Fim da subfase de Identificação de paginas Brancas na Separacao [{0}]", this.GetLocalIpAddress());
                        }

                        if (LogLote.AcaoSeparacaoSubfaseThumbnail == subfase)
                        {
                            observacao = string.Format("Fim da fase de Geração de Thumbs na Separacao [{0}]", this.GetLocalIpAddress());
                        }

                        if (LogLote.AcaoSeparacaoSubfaseOrientacao == subfase)
                        {
                            observacao = string.Format("Fim da fase de Correção de Orientação na Separacao [{0}]", this.GetLocalIpAddress());
                        }

                        if (LogLote.AcaoSeparacaoSubfaseClassificacao == subfase)
                        {
                            observacao = string.Format("Fim da fase de Classificação na Separacao [{0}]", this.GetLocalIpAddress());
                        }

                        this.gravaLogDoLoteServico.Executar(subfase, loteId, observacao);
                    });
                }
                catch (Exception exception)
                {
                    Log.Application.Error(exception);
                }
            });
        }

        private string GetLocalIpAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }
    }
}
