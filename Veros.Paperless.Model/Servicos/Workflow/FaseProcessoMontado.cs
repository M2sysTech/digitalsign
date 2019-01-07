namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class FaseProcessoMontado : FaseDeWorkflow<Processo, ProcessoStatus>
    {
        private readonly IGravaLogDoLoteServico gravaLogDoLoteServico;
        private readonly ICampoRepositorio campoRepositorio;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IRegraRepositorio regraRepositorio;

        public FaseProcessoMontado(
            IGravaLogDoLoteServico gravaLogDoLoteServico, 
            ICampoRepositorio campoRepositorio,
             IRegraVioladaRepositorio regraVioladaRepositorio,
            IRegraRepositorio regraRepositorio)
        {
            this.gravaLogDoLoteServico = gravaLogDoLoteServico;
            this.campoRepositorio = campoRepositorio;
            this.FaseEstaAtiva = x => x.MontagemAtivo;
            this.StatusDaFase = ProcessoStatus.Montado;
            this.StatusSeFaseEstiverInativa = ProcessoStatus.SetaDigitacao;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.regraRepositorio = regraRepositorio;
        }

        protected override void ProcessarFase(Processo processo)
        {
            if (processo.Lote.Status != LoteStatus.Montado)
            {
                return;
            }

            if (this.EstruturaEstaCorreta(processo) == false)
            {
                return;
            }
            
            ////if (processo.Documentos.Any(x => x.Status == DocumentoStatus.AguardandoMontagem))
            ////{
            ////    return;
            ////}

            foreach (var documento in processo.Documentos.Where(x => x.Status != DocumentoStatus.Excluido))
            {
                this.AjustarMarcacaoDeDigitacaoDeIndexadores(documento);

                var campos = this.campoRepositorio.ObterPorCodigoTipoDocumento(documento.TipoDocumento.Id);
                documento.AdicionarIndexadoresFaltantes(campos);
            }

            processo.Status = ProcessoStatus.SetaDigitacao;
            processo.Lote.DataFimDaMontagem = DateTime.Now;
        }

        private void AjustarMarcacaoDeDigitacaoDeIndexadores(Documento documento)
        {
            ////TODO: Provavelmente essa rotina deve estar em outro serviço
            foreach (var indexacao in documento.Indexacao)
            {
                if (string.IsNullOrEmpty(indexacao.PrimeiroValor) == false && indexacao.UsuarioPrimeiroValor == 0)
                {
                    indexacao.UsuarioPrimeiroValor = -2;
                    indexacao.DataPrimeiraDigitacao = DateTime.Now;
                }

                if (string.IsNullOrEmpty(indexacao.PrimeiroValor))
                {
                    indexacao.UsuarioPrimeiroValor = 0;
                    indexacao.DataPrimeiraDigitacao = null;
                }

                if (string.IsNullOrEmpty(indexacao.SegundoValor) == false && indexacao.UsuarioSegundoValor == 0)
                {
                    indexacao.UsuarioSegundoValor = -2;
                    indexacao.DataSegundaDigitacao = DateTime.Now;
                }

                if (string.IsNullOrEmpty(indexacao.SegundoValor))
                {
                    indexacao.UsuarioSegundoValor = 0;
                    indexacao.DataSegundaDigitacao = null;
                }
            }
        }

        private bool EstruturaEstaCorreta(Processo processo)
        {
            return true;

            var quantidadeDePacs = processo.Documentos.Count(x => x.TipoDocumento.IsPac);
            var estruturaCorreta = true;

            if (quantidadeDePacs == 0)
            {
                this.gravaLogDoLoteServico.Executar(
                    LogLote.AcaoDocumentoObrigatorioNaoEncontrado,
                    processo.Lote.Id,
                    "Estrutura do processo inválida. Não foi localizado o documento obrigatório do tipo [PAC].",
                    "ta_liberado");

                processo.Status = ProcessoStatus.Erro;
                processo.AlterarStatusDosDocumentos(DocumentoStatus.Erro);

                estruturaCorreta = false;
            }

            if (quantidadeDePacs > 1)
            {
                this.SalvarRegraViolada(processo, "Estrutura do processo inválida. Existe mais de uma [PAC] presente no processo.");
                this.gravaLogDoLoteServico.Executar(
                    LogLote.AcaoDocumentoDuplicado,
                    processo.Lote.Id,
                    "Estrutura do processo inválida. Existe mais de uma PAC presente no processo.",
                    "ta_liberado");
                processo.Status = ProcessoStatus.Erro;
                processo.AlterarStatusDosDocumentos(DocumentoStatus.Erro);

                estruturaCorreta = false;
            }

            if (estruturaCorreta)
            {
                var documentos = processo.Documentos.Where(x => x.TipoDocumento.IsPac).Where(documento => documento.Paginas.Count < documento.TipoDocumento.QuantidadeDePaginas);

                foreach (var documento in documentos)
                {
                    this.SalvarRegraViolada(processo, "Estrutura do processo inválida. Falta página no documento do tipo [PAC].", documento);

                    this.gravaLogDoLoteServico.Executar(
                        LogLote.AcaoLoteFaltaPaginaNaPac,
                        processo.Lote.Id,
                        "Estrutura do processo inválida. Falta página no documento do tipo [PAC].",
                        "ta_liberado");

                    processo.Status = ProcessoStatus.SetaValidacao;
                    processo.AlterarStatusDosDocumentos(DocumentoStatus.ParaValidacao);

                    estruturaCorreta = false;
                }
            }

            return estruturaCorreta;
        }

        private void SalvarRegraViolada(Processo processo, string observacao, Documento documento = null)
        {
            var regraVioladaCode = this.regraRepositorio.ObterRegraPorIdentificador("ESTR");

            if (regraVioladaCode != null)
            {
                this.regraVioladaRepositorio.Salvar(new RegraViolada()
                {
                    Processo = processo,
                    Observacao = observacao,
                    SomaDoBinario = 0,
                    Regra = regraVioladaCode,
                    Vinculo = "0",
                    Status = RegraVioladaStatus.Marcada,
                    Documento = documento
                });
            } 
        }
    }
}
