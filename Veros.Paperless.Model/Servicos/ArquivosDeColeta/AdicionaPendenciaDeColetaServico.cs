namespace Veros.Paperless.Model.Servicos.ArquivosDeColeta
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;

    public class AdicionaPendenciaDeColetaServico : IAdicionaPendenciaDeColetaServico
    {
        public void AddPendenciaDeArquivo(ArquivoColeta arquivo, string texto)
        {
            this.CriarPendencia(arquivo, PendenciaColeta.TipoArquivoCsv, texto);
        }

        public void AddPendenciaDeCaixa(ArquivoColeta arquivo, string texto)
        {
            this.CriarPendencia(arquivo, PendenciaColeta.TipoCaixa, texto);
        }

        public void AddPendenciaDeDossie(ArquivoColeta arquivo, string texto)
        {
            this.CriarPendencia(arquivo, PendenciaColeta.TipoDossie, texto);
        }

        public void AddPendenciaDeDossie(ArquivoColeta arquivoDeColeta, DossieEsperado dossieNoBanco, DossieEsperado dossieEsperadoNovo)
        {
            var pendenciaNova = this.CriarPendencia(arquivoDeColeta, PendenciaColeta.TipoDossie, string.Empty);

            pendenciaNova.ProcessoBd = dossieNoBanco.IdentificacaoFormatada();
            pendenciaNova.FolderBd = dossieNoBanco.CodigoDeBarras;
            pendenciaNova.CaixaBd = dossieNoBanco.Pacote.Identificacao;
            pendenciaNova.StatusBd = dossieNoBanco.LoteStatus();
            pendenciaNova.ColetaBd = dossieNoBanco.Coleta;

            pendenciaNova.ProcessoCsv = dossieEsperadoNovo.IdentificacaoFormatada();
            pendenciaNova.FolderCsv = dossieEsperadoNovo.CodigoDeBarras;
            pendenciaNova.CaixaCsv = dossieEsperadoNovo.Identificacao;
        }

        public void AtualizaQuantidadeDeCaixas(ArquivoColeta arquivoColeta, int quantidade)
        {
            arquivoColeta.QuantidadeDeCaixas += quantidade;
            PendenciaColeta pendencia = arquivoColeta.Pendencias.FirstOrDefault(x => x.SubTipo == PendenciaColeta.SubTipoQuantidadeDeCaixas);

            if (pendencia != null)
            {
                pendencia.Texto = string.Format("Caixas OK : {0}", arquivoColeta.QuantidadeDeCaixas);
                return;
            }

            this.CriarPendencia(arquivoColeta, PendenciaColeta.TipoCaixa, "Caixas OK : " + quantidade, PendenciaColeta.SubTipoQuantidadeDeCaixas);
        }

        public void AtualizaQuantidadeDeCaixasDuplicadas(ArquivoColeta arquivoColeta)
        {
            arquivoColeta.QuantidadeDeCaixasDuplicadas++;
            PendenciaColeta pendencia = arquivoColeta.Pendencias.FirstOrDefault(x => x.SubTipo == PendenciaColeta.SubTipoQuantidadeDeCaixasDuplicadas);

            if (pendencia != null)
            {
                pendencia.Texto = string.Format("Caixas em duplicidade : {0}", arquivoColeta.QuantidadeDeCaixasDuplicadas);
                return;
            }

            this.CriarPendencia(arquivoColeta, PendenciaColeta.TipoCaixa, "Caixas em duplicidade : 1", PendenciaColeta.SubTipoQuantidadeDeCaixasDuplicadas);
        }

        public void AtualizaQuantidadeDeDossiesIncluidos(ArquivoColeta arquivoColeta, int quantidade)
        {
            arquivoColeta.QuantidadeDeDossies += quantidade;
            PendenciaColeta pendencia = arquivoColeta.Pendencias.FirstOrDefault(x => x.SubTipo == PendenciaColeta.SubTipoQuantidadeDeDossies);

            if (pendencia != null)
            {
                pendencia.Texto = string.Format("Dossies OK : {0}", arquivoColeta.QuantidadeDeDossies);
                return;
            }

            this.CriarPendencia(arquivoColeta, PendenciaColeta.TipoDossie, "Dossies OK : " + arquivoColeta.QuantidadeDeDossies, PendenciaColeta.SubTipoQuantidadeDeDossies);
        }

        public PendenciaColeta CriarPendencia(ArquivoColeta arquivo, string tipoDePendencia, string texto, string subTipo = "")
        {
            if (arquivo == null || arquivo.Id < 1)
            {
                Log.Application.ErrorFormat("Erro ao registrar pendência de coleta: ArquivoColeta não encontrado na base");
                return null;
            }

            var pendencia = new PendenciaColeta
            {
                ArquivoColeta = arquivo,
                Tipo = tipoDePendencia,
                Texto = texto,
                Ordem = arquivo.Pendencias.Count(x => x.Tipo == tipoDePendencia) + 1,
                DataAnalise = DateTime.Now,
                StatusDaPendencia = PendenciaColeta.StatusAtiva,
                SubTipo = subTipo
            };

            arquivo.Pendencias.Add(pendencia);
            arquivo.Status = ArquivoColeta.PendenciasDetectadas;

            return pendencia;
        }
    }
}
