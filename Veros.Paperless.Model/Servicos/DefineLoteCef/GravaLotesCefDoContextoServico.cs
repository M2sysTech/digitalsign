namespace Veros.Paperless.Model.Servicos.DefineLoteCef
{
    using System;
    using System.Linq;
    using Consultas;
    using Entidades;
    using Framework;
    using Repositorios;

    public class GravaLotesCefDoContextoServico : IGravaLotesCefDoContextoServico
    {
        private readonly ILoteCefRepositorio loteCefRepositorio;
        private readonly IAtualizaAmostraParaQualidadeCefServico atualizaAmostraParaQualidadeCefServico;
        private readonly ILoteCefProntoParaFecharConsulta loteCefProntoParaFecharConsulta;
        private readonly ILoteRepositorio loteRepositorio;

        public GravaLotesCefDoContextoServico(ILoteCefRepositorio loteCefRepositorio,             
            IAtualizaAmostraParaQualidadeCefServico atualizaAmostraParaQualidadeCefServico, 
            ILoteCefProntoParaFecharConsulta loteCefProntoParaFecharConsulta, 
            ILoteRepositorio loteRepositorio)
        {
            this.loteCefRepositorio = loteCefRepositorio;
            this.atualizaAmostraParaQualidadeCefServico = atualizaAmostraParaQualidadeCefServico;
            this.loteCefProntoParaFecharConsulta = loteCefProntoParaFecharConsulta;
            this.loteRepositorio = loteRepositorio;
        }

        public void Executar()
        {
            var configuracao = Contexto.ConfiguracaoDeLoteCef;
            var lotesCefAbertos = this.loteCefRepositorio.ObterAbertos(configuracao);
            var contador = 0;

            foreach (var loteCefAberto in lotesCefAbertos.OrderBy(x => x.Id))
            {
                //// isso so esta aqui para compatibilizar com versao antigo, que vai ter 2 lotes abertos
                //// depois que essa versão subir, isso perde o sentido. Só var ter 1 lotecef Aberto por vez. 
                var prioridadeLoteAbertoFaltandoItens = contador == 0;
                contador++;

                var lotecefPronto = this.loteCefProntoParaFecharConsulta.ObterQuantidades(loteCefAberto.Id).FirstOrDefault();
                if (lotecefPronto.QtdeTotal >= configuracao.QuantidadeMaxima)
                {
                    if (lotecefPronto.QtdeQualiCef >= configuracao.QuantidadeMaxima * (Contexto.QualidadePorcentagemCef / 100.0))
                    {
                        //// finaliza, abre outro, move excesso, reseta variaveis
                        this.TratarFinalizacaoLotecef(lotecefPronto, configuracao.QuantidadeMaxima);
                        return;
                    }
                    else
                    {
                        //// tenta preencher com qualiM2sys
                        if (this.atualizaAmostraParaQualidadeCefServico.LocalizarLotesAprovadosQualiM2(lotecefPronto.LotecefId, prioridadeLoteAbertoFaltandoItens))
                        {
                            this.TratarFinalizacaoLotecef(lotecefPronto, configuracao.QuantidadeMaxima);
                            return;
                        }
                    }
                }

                if (DateTime.Now.Hour > configuracao.HoraFechamento.GetValueOrDefault().Hour && lotecefPronto.QtdeTotal >= configuracao.QuantidadeMinima)
                {
                    if (lotecefPronto.QtdeQualiCef >= lotecefPronto.QtdeTotal * (Contexto.QualidadePorcentagemCef / 100.0))
                    {
                        //// finaliza, abre outro, move excesso, reseta variaveis
                        this.loteCefRepositorio.Finalizar(lotecefPronto.LotecefId, lotecefPronto.QtdeTotal);
                        return;
                    }
                    else
                    {
                        //// tenta preencher com qualiM2sys
                        if (this.atualizaAmostraParaQualidadeCefServico.LocalizarLotesAprovadosQualiM2(lotecefPronto.LotecefId, prioridadeLoteAbertoFaltandoItens))
                        {
                            this.loteCefRepositorio.Finalizar(lotecefPronto.LotecefId, lotecefPronto.QtdeTotal);
                            return;    
                        }
                    }
                }

                if (loteCefAberto.QuantidadeDeLotes != lotecefPronto.QtdeTotal)
                {
                    this.loteCefRepositorio.AlterarQuantidade(loteCefAberto.Id, lotecefPronto.QtdeTotal);
                }
            }
        }

        public void ExecutarRecusadosAguardandoNovaAmostra()
        {
            var lotesCefAguardando = this.loteCefRepositorio.ObterAguardandoNovaAmostra();

            foreach (var loteCefAguardando in lotesCefAguardando.OrderBy(x => x.Id))
            {
                if (this.JaPodeRedisponibilizarParaQualiCef(loteCefAguardando))
                {
                    this.loteCefRepositorio.RedisponibilizarParaQualicef(loteCefAguardando.Id);
                }
            }
        }

        private bool JaPodeRedisponibilizarParaQualiCef(LoteCef loteCefAguardando)
        {
            var lotes = this.loteRepositorio.ObterPorLoteCef(loteCefAguardando.Id);
            if (lotes.Count == 0)
            {
                return false;
            }

            var maiorAmostra = lotes.Max(x => x.QualidadeCef);
            var lotesNaQualiCef = lotes.Count(x => x.QualidadeCef == maiorAmostra && x.Status == LoteStatus.AguardandoControleQualidadeCef);
            var lotesNaPrimeiraAmostra = lotes.Count(x => x.QualidadeCef == 1);
            return lotesNaQualiCef >= lotesNaPrimeiraAmostra;
        }

        private void TratarFinalizacaoLotecef(LotecefPronto lotecefPronto, int qtdeDesejada)
        {
            //// movendo excessos para novo dia
            if (lotecefPronto.QtdeTotal - qtdeDesejada > 0)
            {
                LoteCef lotecefAberto = null;
                var listagemJaAberto = this.loteCefRepositorio.ObterAbertos();
                if (listagemJaAberto.Count > 0)
                {
                    lotecefAberto = listagemJaAberto.Where(x => x.Id != lotecefPronto.LotecefId).OrderBy(x => x.Id).FirstOrDefault();
                }

                if (lotecefAberto == null) 
                {
                    lotecefAberto = LoteCef.Novo();
                    this.loteCefRepositorio.Salvar(lotecefAberto);
                }

                var contador = 0;
                var lotes = this.loteRepositorio.ObterPorLoteCefId(lotecefPronto.LotecefId).OrderByDescending(x => x.Id);
                foreach (var lote in lotes.Where(x => x.Status == LoteStatus.Faturamento && x.ResultadoQualidadeCef == null && x.QualidadeM2sys == 0))
                {
                    this.loteRepositorio.AtualizarLotecef(lote.Id, lotecefAberto.Id);
                    contador++;
                    if (contador >= lotecefPronto.QtdeTotal - qtdeDesejada)
                    {
                        break;
                    }
                }
            }

            this.loteCefRepositorio.Finalizar(lotecefPronto.LotecefId, qtdeDesejada);
        }

        private bool PodeFecharLotecef(int lotecefId)
        {
            var configuracao = Contexto.ConfiguracaoDeLoteCef;
            var lotecefPronto = this.loteCefProntoParaFecharConsulta.ObterQuantidades(lotecefId).FirstOrDefault();
            if (lotecefPronto == null)
            {
                return false;
            }

            if (lotecefPronto.QtdeTotal >= configuracao.QuantidadeMaxima )
            {
                if (lotecefPronto.QtdeQualiCef >= configuracao.QuantidadeMaxima * (Contexto.QualidadePorcentagemCef / 100.0))
                {
                    return true;
                }
            }

            if (DateTime.Now.Hour > configuracao.HoraFechamento.GetValueOrDefault().Hour  && lotecefPronto.QtdeTotal >= configuracao.QuantidadeMinima)
            {
                if (lotecefPronto.QtdeQualiCef >= lotecefPronto.QtdeTotal * (Contexto.QualidadePorcentagemCef / 100.0))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
