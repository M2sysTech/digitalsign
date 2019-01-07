namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class AtualizaAmostraParaQualidadeCefServico : IAtualizaAmostraParaQualidadeCefServico
    {
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly IConfiguracaoDeLoteCefRepositorio configuracaoDeLoteCefRepositorio;

        public AtualizaAmostraParaQualidadeCefServico( 
            ILoteRepositorio loteRepositorio, 
            IRegraVioladaRepositorio regraVioladaRepositorio,
            IProcessoRepositorio processoRepositorio, 
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            IConfiguracaoDeLoteCefRepositorio configuracaoDeLoteCefRepositorio)
        {
            this.loteRepositorio = loteRepositorio;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.processoRepositorio = processoRepositorio;
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.configuracaoDeLoteCefRepositorio = configuracaoDeLoteCefRepositorio;
        }

        public bool Executar(int loteCefId)
        {
            var lotes = this.loteRepositorio.ObterPorLoteCefId(loteCefId);

            var quantidadeDeLotesNaQualidadeCef = lotes.Count(x => x.Status == LoteStatus.AguardandoControleQualidadeCef);
            var quantidadeDeLotesEsperado = (int) Math.Truncate(lotes.Count * (Contexto.QualidadePorcentagemCef / 100.0));
            var limiteInferior = quantidadeDeLotesEsperado - 1;

            if (quantidadeDeLotesNaQualidadeCef >= limiteInferior)
            {
                return true;
            }

            if (this.LotesAprovadosQualidadeM2SysSemMarcas(lotes, ref quantidadeDeLotesNaQualidadeCef,
                quantidadeDeLotesEsperado, limiteInferior))
            {
                return true; 
            }
            
            if (this.LotesAprovadosQualidadeM2Sys(lotes, ref quantidadeDeLotesNaQualidadeCef, limiteInferior, 0, false))
            {
                return true;
            }

            if (this.LotesNaFaseDeFaturamento(lotes, ref quantidadeDeLotesNaQualidadeCef,
                quantidadeDeLotesEsperado, limiteInferior))
            {
                return true;
            }

            return false;
        }

        public bool LocalizarLotesAprovadosQualiM2(int lotecefId, bool prioridade = false)
        {
            var configNoBanco = this.configuracaoDeLoteCefRepositorio.ObterUnico();

            var lotes = this.loteRepositorio.ObterPorLoteCefId(lotecefId);

            var qtdeDossiesLote = lotes.Count > configNoBanco.QuantidadeMaxima ? configNoBanco.QuantidadeMaxima : lotes.Count;

            var quantidadeDeLotesNaQualidadeCef = lotes.Count(x => x.QualidadeCef == 1);
            var quantidadeDeLotesEsperado = (int)Math.Truncate(qtdeDossiesLote * (Contexto.QualidadePorcentagemCef / 100.0));
            var limiteInferior = quantidadeDeLotesEsperado;

            if (quantidadeDeLotesNaQualidadeCef >= limiteInferior)
            {
                return true;
            }

            if (this.LotesAprovadosQualidadeM2Sys(lotes, ref quantidadeDeLotesNaQualidadeCef, limiteInferior, lotecefId, prioridade))
            {
                return true;
            }

            return false;
        }

        private bool LotesAprovadosQualidadeM2SysSemMarcas(IList<Lote> lotesCef, 
            ref int quantidadeLotesNaQualidadeCef,
            int quantidadeDeLotesEsperado,
            int limiteInferior)
        {
            var lotesEmFaturamento = lotesCef.Where(x => x.Status == LoteStatus.Faturamento &&
                                                    x.QualidadeM2sys == 1).ToList();

            foreach (var lote in lotesEmFaturamento)
            {
                var loteSemMarca = this.regraVioladaRepositorio.ExisteRegraVioladaPorProcesso(lote.Processos.First().Id);

                if (loteSemMarca == false)
                {
                    this.loteRepositorio.EnviarParaQualidadeCef(lote.Id, 1);
                    this.processoRepositorio.EnviarParaQualidadeCef(lote.Id);

                    this.gravaLogDoLoteServico.Executar(LogLote.AcaoLotesAprovadosQualidadeM2SysSemMarcas,
                    lote.Id, string.Format("Dossiê enviado para Qualidade CEF [Aprovados na Qualidade M2sys e sem marcas]")); 

                    quantidadeLotesNaQualidadeCef++;
                }

                if (quantidadeLotesNaQualidadeCef == quantidadeDeLotesEsperado || 
                    quantidadeLotesNaQualidadeCef >= limiteInferior)
                {
                    return true;
                }
            }

            return false;
        }

        private bool LotesAprovadosQualidadeM2Sys(IList<Lote> lotesCef, ref int quantidadeLotesNaQualidadeCef, int limiteInferior, int lotecefId, bool prioridade)
        {
            var lotesEmFaturamento = lotesCef.Where(x => x.Status == LoteStatus.Faturamento && 
                                                    x.QualidadeM2sys == 1).ToList();

            foreach (var lote in lotesEmFaturamento)
            {
                this.loteRepositorio.EnviarParaQualidadeCef(lote.Id, 1);
                this.processoRepositorio.EnviarParaQualidadeCef(lote.Id);

                this.gravaLogDoLoteServico.Executar(LogLote.AcaoLotesAprovadosQualidadeM2Sys,
                    lote.Id, string.Format("Dossiê enviado para Qualidade CEF [Aprovados na Qualidade M2sys]")); 

                quantidadeLotesNaQualidadeCef++;

                if (quantidadeLotesNaQualidadeCef >= limiteInferior)
                {
                    return true;    
                }
            }

            //// pega de outros lotecef entao...
            if (prioridade)
            {
                var lotesDeOutros = this.loteRepositorio.ObterLotesAbertosComQualiM2Sys();
                foreach (var loteExterno in lotesDeOutros.Where(x => x.QualidadeM2sys == 1 && (x.QualidadeCef < 1)))
                {
                    this.loteRepositorio.EnviarParaQualidadeMudandoLote(loteExterno.Id, lotecefId);
                    this.processoRepositorio.EnviarParaQualidadeCef(loteExterno.Id);

                    this.gravaLogDoLoteServico.Executar(LogLote.AcaoLotesAprovadosQualidadeM2Sys, loteExterno.Id, string.Format("Dossiê enviado para Qualidade CEF [Aprovados na Qualidade M2sys] - Lotecef:{0}", lotecefId));

                    quantidadeLotesNaQualidadeCef++;
                    if (quantidadeLotesNaQualidadeCef >= limiteInferior)
                    {
                        //// todo: resetar contagem de contexto do Wflow 
                        return true;
                    }
                }
            }

            return false;
        }

        private bool LotesNaFaseDeFaturamento(IList<Lote> lotesCef,
            ref int quantidadeLotesNaQualidadeCef, 
            int quantidadeDeLotesEsperado,
            int limiteInferior)
        {
            var lotesEmFaturamento = lotesCef.Where(x => x.Status == LoteStatus.Faturamento).ToList();

            foreach (var lote in lotesEmFaturamento)
            {
                this.loteRepositorio.EnviarParaQualidadeCef(lote.Id, 1);
                this.processoRepositorio.EnviarParaQualidadeCef(lote.Id);

                this.gravaLogDoLoteServico.Executar(LogLote.AcaoLotesNaFaseDeFaturamento,
                    lote.Id, string.Format("Dossiê enviado para Qualidade CEF [Na fase de faturamento]")); 

                quantidadeLotesNaQualidadeCef++;

                if (quantidadeLotesNaQualidadeCef == quantidadeDeLotesEsperado ||
                    quantidadeLotesNaQualidadeCef >= limiteInferior)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
