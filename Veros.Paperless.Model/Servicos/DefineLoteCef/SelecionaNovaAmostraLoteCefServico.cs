namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class SelecionaNovaAmostraLoteCefServico : ISelecionaNovaAmostraLoteCefServico
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoteCefRepositorio loteCefRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public SelecionaNovaAmostraLoteCefServico(
            IUnitOfWork unitOfWork,
            ILoteCefRepositorio loteCefRepositorio, 
            ILoteRepositorio loteRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.unitOfWork = unitOfWork;
            this.loteCefRepositorio = loteCefRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public void Executar()
        {
            var lotesCefReprovados = this.unitOfWork.Obter(() => this.loteCefRepositorio.ObterReprovados());

            foreach (var loteCef in lotesCefReprovados)
            {
                try
                {
                    using (this.unitOfWork.Begin())
                    {
                        this.Processar(loteCef);
                    }
                }
                catch (Exception exception)
                {
                    Log.Application.ErrorFormat("Erro ao selecionar nova amostra para qualidade CEF: {0}", exception);
                }
            }
        }

        private void Processar(LoteCef loteCef)
        {
            var lotes = this.loteRepositorio.ObterPorLoteCef(loteCef.Id);
            var identificacaoNovaAmostra = lotes.Max(x => x.QualidadeCef) + 1;

            if (identificacaoNovaAmostra > 6)
            {
                Log.Application.InfoFormat("Não buscar nova amostra pois o LoteCEF #{0} já possui mais de 6 amostras.", loteCef.Id);
                return;
            }

            var quantidadeDeLotesParaNovaAmostra = lotes.Count(x => x.QualidadeCef == 1);
            var novaAmostra = lotes.Where(x => x.QualidadeM2sys == 1 && x.QualidadeCef == 0 && x.Status == LoteStatus.Faturamento).ToList();

            ////if (novaAmostra.Count() >= quantidadeDeLotesParaNovaAmostra)
            ////{
            ////    this.EnviarParaQualidade(
            ////        novaAmostra.Take(quantidadeDeLotesParaNovaAmostra).ToList(),
            ////        identificacaoNovaAmostra);

            ////    this.loteCefRepositorio.AlterarStatus(loteCef.Id, LoteCefStatus.Fechado);
            ////    return;
            ////}

            /////*
            //// * OK - os selecionados faz: pega a quantidade e seta LoteSTatus = q5 e proc_status = q5 e batch_qualicef = identificacaoNovaAmostra
            //// * OK - ate chegar no numero quantidadeDeLotesParaNovaAmostra, vou enviar para qualidade M2: batch_m2 = 1 e  LoteSTatus = M5 e proc_status = M5
            //// * 
            //// * TODOS ESSES DEVE RECEBER BATCH_QUALICEF = identificacaoNovaAmostra
            //// * 
            //// * lotecef_status = ReprovadoAguardandoNovaAmostra
            //// */

            ////this.EnviarParaQualidade(novaAmostra, identificacaoNovaAmostra);
            this.EnviarLotesParaQualidadeM2(lotes, identificacaoNovaAmostra, quantidadeDeLotesParaNovaAmostra);
            this.loteCefRepositorio.AlterarStatus(loteCef.Id, LoteCefStatus.ReprovadoAguardandoNovaAmostra);
        }

        private void EnviarLotesParaQualidadeM2(IList<Lote> lotes, int identificacaoNovaAmostra, int quantidade)
        {
            var amostraJaQualidade = lotes.Where(x => x.QualidadeM2sys == 1 && x.QualidadeCef == 0 && x.Status == LoteStatus.Faturamento).ToList();
            var amostraSemQualidade = lotes.Where(x => x.QualidadeM2sys == 0 && x.QualidadeCef == 0 && x.Status == LoteStatus.Faturamento).ToList();

            if (quantidade > amostraSemQualidade.Count + amostraJaQualidade.Count)
            {
                throw new RegraDeNegocioException("Erro ao selecionar nova amostra de Lotes para Qualidade CEF. Quantidade disponível é {0}", lotes.Count);
            }

            var selecionados = amostraJaQualidade.Take(quantidade).ToList();
            if (selecionados.Count < quantidade)
            {
                selecionados.AddRange(amostraSemQualidade.Take(quantidade - selecionados.Count).ToList());    
            }

            this.loteRepositorio.AtualizarStatus(selecionados, LoteStatus.AguardandoControleQualidadeM2, identificacaoNovaAmostra);
            this.loteRepositorio.EnviarParaQualidadeM2(selecionados);
            this.processoRepositorio.AlterarStatusPorLote(selecionados, ProcessoStatus.AguardandoControleQualidadeM2);
        }

        private void EnviarParaQualidade(List<Lote> lotes, int identificacaoNovaAmostra)
        {
            if (lotes.Any() == false)
            {
                return;
            }

            this.loteRepositorio.AtualizarStatus(lotes, LoteStatus.AguardandoControleQualidadeCef, identificacaoNovaAmostra);
            this.processoRepositorio.AlterarStatusPorLote(lotes, ProcessoStatus.AguardandoControleQualidadeCef);
        }
    }
}
