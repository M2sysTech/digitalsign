namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections;
    using Framework;

    [Serializable]
    public class ProcessoStatus : EnumerationString<ProcessoStatus>
    {
        public static ProcessoStatus SetaErro = new ProcessoStatus("D0", "Seta Erro");
        public static ProcessoStatus Erro = new ProcessoStatus("DE", "Erro");

        public static ProcessoStatus SetaTransmissao = new ProcessoStatus("30", "Seta Transmissao");
        public static ProcessoStatus AguardandoTransmissao = new ProcessoStatus("35", "Aguardando Transmissao");
        public static ProcessoStatus AguardandoRecaptura = new ProcessoStatus("36", "Aguardando Recaptura");
        public static ProcessoStatus Transmitido = new ProcessoStatus("3A", "Transmitido");

        public static ProcessoStatus AguardandoSeparacao = new ProcessoStatus("40", "Aguardando Separacao Documentos");        

        public static ProcessoStatus SetaTriagem = new ProcessoStatus("T0", "Aguardando Triagem Pre-OCR");
        public static ProcessoStatus AguardandoTriagem = new ProcessoStatus("T5", "Aguardando Triagem Pre-OCR");
        public static ProcessoStatus TriagemFinalizada = new ProcessoStatus("TA", "Triagem Pre-OCR Finalizada");

        public static ProcessoStatus AguardandoMontagemPdf = new ProcessoStatus("45", "Aguardando Montagem Pdf");
        public static ProcessoStatus PdfMontado = new ProcessoStatus("4A", "Pdf Montado");

        public static ProcessoStatus AguardandoIdentificacao = new ProcessoStatus("62", "Aguardando Identificação");

        public static ProcessoStatus AguardandoMontagem = new ProcessoStatus("65", "Aguardando Montagem Manual");
        public static ProcessoStatus Montado = new ProcessoStatus("6A", "Montagem Concluída");
        
        public static ProcessoStatus SetaDigitacao = new ProcessoStatus("70", "Seta Digitação");
        public static ProcessoStatus AguardandoDigitacao = new ProcessoStatus("75", "Aguardando Digitação");
        public static ProcessoStatus Digitado = new ProcessoStatus("7A", "Digitado");
        
        public static ProcessoStatus SetaConsulta = new ProcessoStatus("80", "Seta Consulta");
        public static ProcessoStatus AguardandoConsulta = new ProcessoStatus("85", "Aguardando Consulta");
        public static ProcessoStatus Consultado = new ProcessoStatus("8A", "Consultado");

        public static ProcessoStatus SetaValidacao = new ProcessoStatus("90", "Seta Validação");
        public static ProcessoStatus AguardandoValidacao = new ProcessoStatus("95", "Aguardando Validação");
        public static ProcessoStatus Validado = new ProcessoStatus("9A", "Validado");

        public static ProcessoStatus SetaProvaZero = new ProcessoStatus("A0", "Seta ProvaZero");
        public static ProcessoStatus AguardandoProvaZero = new ProcessoStatus("A5", "Aguardando ProvaZero");
        public static ProcessoStatus ProvaZeroRealizada = new ProcessoStatus("AA", "ProvaZero Realizada");

        public static ProcessoStatus SetaFormalistica = new ProcessoStatus("B0", "Seta Formalística");
        public static ProcessoStatus AguardandoFormalistica = new ProcessoStatus("B5", "Aguardando Formalística");
        public static ProcessoStatus FormalisticaRealizada = new ProcessoStatus("BA", "Formalística Realizada");

        public static ProcessoStatus SetaControleQualidadeM2 = new ProcessoStatus("M1", "Seta Controle de Qualidade M2");
        public static ProcessoStatus AguardandoControleQualidadeM2 = new ProcessoStatus("M5", "Aguardando Controle Qualidade M2");
        public static ProcessoStatus ControleQualidadeM2Finalizado = new ProcessoStatus("MA", "Controle Qualidade M2 Finalizado");

        public static ProcessoStatus AguardandoAssinatura = new ProcessoStatus("82", "Aguardando Assinatura");
        public static ProcessoStatus AssinaturaFinalizada = new ProcessoStatus("83", "Assinatura Finalizada");

        public static ProcessoStatus SetaControleQualidadeCef = new ProcessoStatus("Q1", "Seta Controle de Qualidade Cef");
        public static ProcessoStatus AguardandoControleQualidadeCef = new ProcessoStatus("Q5", "Aguardando Controle Qualidade Cef");
        public static ProcessoStatus ControleQualidadeCefFinalizado = new ProcessoStatus("QA", "Controle Qualidade Cef Finalizado");

        public static ProcessoStatus SetaRevisao = new ProcessoStatus("B1", "Seta Revisão");
        public static ProcessoStatus AguardandoRevisao = new ProcessoStatus("B6", "Aguardando Revisão");
        public static ProcessoStatus RevisaoRealizada = new ProcessoStatus("BB", "Revisão Realizada");

        public static ProcessoStatus SetaAprovacao = new ProcessoStatus("C0", "Seta Aprovação");
        public static ProcessoStatus AguardandoAprovacao = new ProcessoStatus("C5", "Aguardando Aprovação");
        public static ProcessoStatus AguardandoAprovacaoEspecial = new ProcessoStatus("C6", "Aguardando Aprovação Especial");
        public static ProcessoStatus Aprovado = new ProcessoStatus("CA", "Aprovado");

        public static ProcessoStatus SetaExportacao = new ProcessoStatus("F0", "Seta Exportação");
        public static ProcessoStatus AguardandoExportacao = new ProcessoStatus("F5", "Aguardando Exportação");

        public static ProcessoStatus ExportacaoRealizada = new ProcessoStatus("FA", "Exportação Realizada");

        public static ProcessoStatus Faturamento = new ProcessoStatus("H0", "Faturamento");

        public static ProcessoStatus AguardandoEnvio = new ProcessoStatus("H5", "Aguardando Envio");
        public static ProcessoStatus EnvioRealizado = new ProcessoStatus("HA", "Envio Realizado");

        public static ProcessoStatus SetaPreparacaoAjustes = new ProcessoStatus("K1", "Seta Preparação Ajustes");
        public static ProcessoStatus AguardandoPreparacaoAjustes = new ProcessoStatus("K5", "Aguardando Preparação Ajustes");
        public static ProcessoStatus PreparacaoAjustesConcluidos = new ProcessoStatus("KA", "Preparação Ajustes Concluido");

        public static ProcessoStatus SetaAjuste = new ProcessoStatus("J0", "Seta Ajuste");
        public static ProcessoStatus AguardandoAjuste = new ProcessoStatus("J5", "Aguardando Ajuste");
        public static ProcessoStatus AjusteSolicitado = new ProcessoStatus("JA", "Ajuste Solicitado");
        public static ProcessoStatus AjusteConcluido = new ProcessoStatus("JX", "Ajuste Concluido");

        ////public static ProcessoStatus SetaExpurgo = new ProcessoStatus("J0", "Seta Expurgo");
        ////public static ProcessoStatus AguardandoExpurgo = new ProcessoStatus("J5", "Aguardando Expurgo");
        ////public static ProcessoStatus ExpurgoRealizado = new ProcessoStatus("JA", "Expurgo Realizado");
        
        public static ProcessoStatus SetaRetorno = new ProcessoStatus("I0", "Seta Retorno");
        public static ProcessoStatus AguardandoRetorno = new ProcessoStatus("I5", "Aguardando Retorno");
        public static ProcessoStatus RetornoFinalizado = new ProcessoStatus("IA", "Retorno Realizado");

        public static ProcessoStatus Finalizado = new ProcessoStatus("G0", "Fechado");
        public static ProcessoStatus Excluido = new ProcessoStatus("*", "Excluido");

        public static ProcessoStatus[] ListaDeStatusParaWorklofowProcessar = new[]
        {
            ProcessoStatus.PdfMontado,
            ProcessoStatus.AguardandoMontagem,
            ProcessoStatus.Montado,
            ProcessoStatus.SetaDigitacao,
            ProcessoStatus.AguardandoDigitacao,
            ProcessoStatus.AguardandoValidacao,
            ProcessoStatus.SetaValidacao,
            ProcessoStatus.AguardandoProvaZero,
            ProcessoStatus.AguardandoFormalistica,
            ProcessoStatus.AguardandoRevisao,
            ProcessoStatus.AguardandoAssinatura,
            ProcessoStatus.AssinaturaFinalizada,
            ProcessoStatus.SetaAprovacao,
            ProcessoStatus.Aprovado,
            ProcessoStatus.Validado,
            ProcessoStatus.SetaExportacao,
            ProcessoStatus.ExportacaoRealizada,
            ProcessoStatus.Faturamento,
            ProcessoStatus.EnvioRealizado,
            ProcessoStatus.SetaRetorno,
            ProcessoStatus.RetornoFinalizado,
            ProcessoStatus.SetaErro
        };

        public static ProcessoStatus[] ListaDeStatusParaClassifierProcessar = new[]
        {
            ProcessoStatus.AguardandoMontagem,
            ProcessoStatus.Montado,
            ProcessoStatus.SetaDigitacao,
            ProcessoStatus.AguardandoDigitacao,
            ProcessoStatus.AguardandoValidacao,
            ProcessoStatus.SetaValidacao,
            ProcessoStatus.AguardandoProvaZero
        };
        
        public ProcessoStatus(string value, string displayName) : base(value, displayName)
        {
        }
    }
}
