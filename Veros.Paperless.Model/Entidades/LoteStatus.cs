namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Linq;
    using Framework;

    [Serializable]
    public class LoteStatus : EnumerationString<LoteStatus>
    {
        public static LoteStatus EmCaptura = new LoteStatus("10", "Em Captura");
        public static LoteStatus EmRecepcao = new LoteStatus("12", "Captura Importando");
        public static LoteStatus EmTransmissao = new LoteStatus("13", "Em Transmissao");
        public static LoteStatus CapturaFinalizada = new LoteStatus("15", "Captura Finalizada");

        public static LoteStatus AguardandoSeparacaoClassifier = new LoteStatus("35", "Aguardando Separacao");

        public static LoteStatus SetaTriagem = new LoteStatus("T0", "Seta Triagem Pre-OCR");
        public static LoteStatus AguardandoTriagem = new LoteStatus("T5", "Aguardando Triagem Pre-OCR");
        public static LoteStatus TriagemFinalizada = new LoteStatus("TA", "Triagem Pre-OCR Finalizada");

        public static LoteStatus AguardandoIdentificacaoManual = new LoteStatus("I5", "Aguardando Identificação Manual");

        public static LoteStatus SetaClassifier = new LoteStatus("40", "Seta Classifier");
        public static LoteStatus AguardandoClassifier = new LoteStatus("45", "Aguardando Classifier");
        public static LoteStatus ClassifierExecutado = new LoteStatus("4A", "Classifier Concluido");

        public static LoteStatus SetaReconhecimento = new LoteStatus("50", "Seta Reconhecimento");
        public static LoteStatus AguardandoReconhecimento = new LoteStatus("55", "Aguardando Reconhecimento");
        public static LoteStatus ReconhecimentoExecutado = new LoteStatus("5A", "Reconhecido Concluido");

        public static LoteStatus ParaTransmitir = new LoteStatus("20", "Para Transmitir");
        public static LoteStatus AguardandoTransmissao = new LoteStatus("30", "Aguardando Transmissão");
        public static LoteStatus AguardandoRetransmissaoImagem = new LoteStatus("3X", "Aguardando Retransmissão de Imagem");

        public static LoteStatus AguardandoBatimento = new LoteStatus("5S", "Aguardando Batimento");
        public static LoteStatus Batido = new LoteStatus("5T", "Batido");
        
        public static LoteStatus SetaIdentificacao = new LoteStatus("61", "Seta Identificação");
        public static LoteStatus AguardandoIdentificacao = new LoteStatus("62", "Aguardando Identificação");
        public static LoteStatus Identificado = new LoteStatus("63", "Identificação Finalizada");

        public static LoteStatus SetaControleQualidadeM2 = new LoteStatus("M1", "Seta Controle de Qualidade M2");
        public static LoteStatus AguardandoControleQualidadeM2 = new LoteStatus("M5", "Aguardando Controle de Qualidade M2");
        public static LoteStatus ControleQualidadeM2Realizado = new LoteStatus("MA", "Controle de Qualidade M2 Finalizado");

        public static LoteStatus AguardandoGeracaoTermos = new LoteStatus("N1", "Aguardando Geração de termos pelo Classifier");
        public static LoteStatus GeracaoTermosExecutado = new LoteStatus("N2", "Geração de termos Concluido");

        public static LoteStatus SetaControleQualidadeCef = new LoteStatus("Q1", "Seta Controle de Qualidade CEF");
        public static LoteStatus AguardandoControleQualidadeCef = new LoteStatus("Q5", "Aguardando Controle de Qualidade CEF");
        public static LoteStatus ControleQualidadeCefRealizado = new LoteStatus("QA", "Controle de Qualidade CEF Finalizado");

        public static LoteStatus SetaPreparacaoAjustes = new LoteStatus("K1", "Seta Preparação Ajustes");
        public static LoteStatus AguardandoPreparacaoAjustes = new LoteStatus("K5", "Aguardando Preparação Ajustes");
        public static LoteStatus PreparacaoAjustesConcluidos = new LoteStatus("KA", "Preparação Ajustes Concluido");

        public static LoteStatus SetaAjustes = new LoteStatus("J1", "Seta Ajustes");
        public static LoteStatus AguardandoAjustes = new LoteStatus("J5", "Aguardando Ajustes");
        public static LoteStatus AguardandoRealizacaoAjustes = new LoteStatus("J6", "Aguardando Realização de Ajustes");
        public static LoteStatus AjustesSolicitados = new LoteStatus("JA", "Ajustes Solicitados");
        public static LoteStatus AjustesConcluidos = new LoteStatus("JX", "Ajustes Concluidos");

        public static LoteStatus SetaRecaptura = new LoteStatus("R1", "Seta Recaptura");
        public static LoteStatus AguardandoRecaptura = new LoteStatus("R5", "Aguardando Recaptura");
        public static LoteStatus RecapturaRelizada = new LoteStatus("RA", "Recaptura Realizada");
        
        public static LoteStatus AguardandoMontagem = new LoteStatus("65", "Para Montagem");
        public static LoteStatus Montado = new LoteStatus("6A", "Montado");

        public static LoteStatus Faturamento = new LoteStatus("H0", "Aguardando Faturamento");
        public static LoteStatus AprovadoCef = new LoteStatus("H1", "Aprovado na QualiCEF");
        public static LoteStatus FaturaEmitida = new LoteStatus("H2", "Fatura Emitida");

        public static LoteStatus AguardandoAssinatura = new LoteStatus("82", "Aguardando Assinatura");
        public static LoteStatus AssinaturaFinalizada = new LoteStatus("83", "Assinatura Finalizada");
        
        public static LoteStatus EmExportacaoParaCloud = new LoteStatus("90", "Em Exportação Para Cloud");
        public static LoteStatus ExportadoParaCloud = new LoteStatus("91", "Exportado Para Cloud");

        public static LoteStatus EmExportacao = new LoteStatus("F5", "Em Exportação");
        
        public static LoteStatus Finalizado = new LoteStatus("G0", "Fechado");
        public static LoteStatus Erro = new LoteStatus("DE", "Erro");
        public static LoteStatus Excluido = new LoteStatus("*", "Excluído");
        
        public static LoteStatus[] ListaDeStatusParaWorklofowProcessar = new[]
        {
            LoteStatus.CapturaFinalizada,
            LoteStatus.AguardandoTransmissao,
            LoteStatus.SetaTriagem,
            LoteStatus.AguardandoIdentificacaoManual,
            LoteStatus.SetaClassifier,
            LoteStatus.AguardandoClassifier,
            LoteStatus.ClassifierExecutado,
            LoteStatus.SetaReconhecimento,
            LoteStatus.AguardandoReconhecimento,
            LoteStatus.ReconhecimentoExecutado,
            LoteStatus.AguardandoAssinatura,
            LoteStatus.AssinaturaFinalizada,
            LoteStatus.SetaIdentificacao,
            LoteStatus.AguardandoIdentificacao,
            LoteStatus.Identificado,
            LoteStatus.Batido,
            LoteStatus.Montado,
            LoteStatus.ExportadoParaCloud,
            LoteStatus.SetaControleQualidadeM2,
            LoteStatus.GeracaoTermosExecutado,
            LoteStatus.SetaControleQualidadeCef,
            LoteStatus.ControleQualidadeCefRealizado,
            LoteStatus.AguardandoPreparacaoAjustes,
            LoteStatus.SetaPreparacaoAjustes,
            LoteStatus.AguardandoRealizacaoAjustes,
            LoteStatus.AjustesConcluidos
        };

        public static LoteStatus[] ListaDeStatusParaExpurgo = new[]
        {
            LoteStatus.Finalizado,
            LoteStatus.Excluido
        };

        public static LoteStatus[] ListaDeStatusParaFiltroPesquisa = new[]
        {
            LoteStatus.EmCaptura,
            LoteStatus.AguardandoRecaptura,
            LoteStatus.EmRecepcao,
            LoteStatus.EmTransmissao,
            LoteStatus.CapturaFinalizada,
            LoteStatus.AguardandoSeparacaoClassifier,
            LoteStatus.AguardandoTriagem,
            LoteStatus.AguardandoIdentificacaoManual,
            LoteStatus.AguardandoClassifier,
            LoteStatus.ClassifierExecutado,
            LoteStatus.AguardandoReconhecimento,
            LoteStatus.AguardandoIdentificacao,
            LoteStatus.AguardandoControleQualidadeM2,
            LoteStatus.AguardandoGeracaoTermos,
            LoteStatus.GeracaoTermosExecutado,
            LoteStatus.AguardandoControleQualidadeCef,
            LoteStatus.AguardandoAjustes,
            LoteStatus.AguardandoRealizacaoAjustes,
            LoteStatus.AguardandoAssinatura,
            LoteStatus.AssinaturaFinalizada,
            LoteStatus.EmExportacaoParaCloud,
            LoteStatus.ExportadoParaCloud,
            LoteStatus.Faturamento
        };

        public LoteStatus(string value, string displayName) : base(value, displayName)
        {
        }

        public static Enumeration<LoteStatus, string> ObterPorSigla(string fase)
        {
            return ListaDeStatusParaFiltroPesquisa.ToList().SingleOrDefault(x => x.Value == fase);
        }
    }
}
