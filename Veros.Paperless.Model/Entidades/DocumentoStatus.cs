namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework;

    /// <summary>
    /// TODO: refatorar: remover status dos nomes
    /// </summary>
    [Serializable]
    public class DocumentoStatus : EnumerationString<DocumentoStatus>
    {
        public static DocumentoStatus TransmissaoOk = new DocumentoStatus("35", "Transmitido");
        public static DocumentoStatus AguardandoClassifier = new DocumentoStatus("45", "Aguardando Classifier");
        public static DocumentoStatus DoneClassifier = new DocumentoStatus("4A", "Classifier Processado");
        public static DocumentoStatus StatusParaReconhecimento = new DocumentoStatus("55", "Seta Reconhecimento");
        public static DocumentoStatus StatusParaReconhecimentoPosAjuste = new DocumentoStatus("5B", "Seta Reconhecimento Pos Ajuste");
        public static DocumentoStatus StatusParaAguardandoReconhecimentoPosAjuste = new DocumentoStatus("5C", "Aguardando Reconhecimento Pos Ajuste");
        public static DocumentoStatus AguardandoMontagem = new DocumentoStatus("65", "Aguardando Montagem");
        public static DocumentoStatus StatusMontagemConcluida = new DocumentoStatus("6A", "Montagem Concluida");
        public static DocumentoStatus AguardandoIdentificacao = new DocumentoStatus("6C", "Aguardando Identificação");
        public static DocumentoStatus IdentificacaoConcluida = new DocumentoStatus("6D", "Identificação Concluída");
        public static DocumentoStatus ParaDigitacao = new DocumentoStatus("75", "Seta Digitacao");
        public static DocumentoStatus StatusDigitado = new DocumentoStatus("7A", "Digitado");
        public static DocumentoStatus SetaConsulta = new DocumentoStatus("80", "Seta Consulta");
        public static DocumentoStatus ConsultaRealizada = new DocumentoStatus("8A", "Consulta Realizada");
        public static DocumentoStatus ParaValidacao = new DocumentoStatus("95", "Aguardando Validação");
        public static DocumentoStatus StatusValidado = new DocumentoStatus("9A", "Validado");
        public static DocumentoStatus AguardandoFormalistica = new DocumentoStatus("B0", "Aguardando Formalística");
        public static DocumentoStatus FormalisticaRealizada = new DocumentoStatus("BA", "Formalística Realizada");
        public static DocumentoStatus AguardandoRevisao = new DocumentoStatus("B1", "Aguardando Revisão");
        public static DocumentoStatus RevisaoRealizada = new DocumentoStatus("BB", "Revisão Realizada");
        public static DocumentoStatus StatusParaProvaZero = new DocumentoStatus("A0", "Seta Prova Zero");
        public static DocumentoStatus StatusEmProvaZero = new DocumentoStatus("A5", "Em Prova Zero");
        public static DocumentoStatus StatusProvaZeroRealizada = new DocumentoStatus("AA", "Prova Zero Realizada");
        public static DocumentoStatus AguardandoAprovacao = new DocumentoStatus("C0", "Aguardando Aprovacao");
        public static DocumentoStatus Aprovado = new DocumentoStatus("CA", "Aprovado");

        public static DocumentoStatus StatusParaReconhecimentoAjusteDeBrancos = new DocumentoStatus("D0", "Aguardando Reconhecimento Pos Avaliacao de Brancos");
        public static DocumentoStatus StatusParaAguardandoReconhecimentoAjusteDeBrancos = new DocumentoStatus("D1", "Aguardando Pdf Pos Avaliacao de Brancos");

        public static DocumentoStatus StatusParaAjusteOrigem = new DocumentoStatus("E0", "Seta Ajuste Origem");
        public static DocumentoStatus StatusParaExportacao = new DocumentoStatus("F0", "Seta Exportacao");
        public static DocumentoStatus StatusParaEnvio = new DocumentoStatus("H0", "Seta Envio");
        public static DocumentoStatus StatusFinalizado = new DocumentoStatus("G0", "Fechado");
        public static DocumentoStatus DocumentoIlegivel = new DocumentoStatus("?", "Ilegivel");
        public static DocumentoStatus StatusDeConsultaPendente = new DocumentoStatus("P", "Para Consulta");
        public static DocumentoStatus StatusDeConsultaRealizado = new DocumentoStatus("R", "Consulta Realizada");
        public static DocumentoStatus SetaRetorno = new DocumentoStatus("I0", "Seta Retorno");
        public static DocumentoStatus Assinado = new DocumentoStatus("J0", "Assinado");
        public static DocumentoStatus ErroAoAssinar = new DocumentoStatus("JE", "Erro ao Assinar");
        public static DocumentoStatus AjustePreparacao = new DocumentoStatus("J1", "Aguardando Preparaçao para Ajuste (PNG)");
        public static DocumentoStatus AjustePreparacaoRealizada = new DocumentoStatus("J2", "Ajuste PNG Criado");
        public static DocumentoStatus TelaAjuste = new DocumentoStatus("J5", "Aguardando tela de Ajuste");
        public static DocumentoStatus TelaAjusteFinalizado = new DocumentoStatus("JA", "Tela Ajuste Finalizado");
        public static DocumentoStatus TelaAjusteSolicitadoSeparacaoManual = new DocumentoStatus("J6", "Solicitado Separação Manual");
        public static DocumentoStatus TelaAjusteSolicitadoRecaptura = new DocumentoStatus("J7", "Solicitado Recaptura");
        public static DocumentoStatus Erro = new DocumentoStatus("DE", "Erro");
        public static DocumentoStatus Excluido = new DocumentoStatus("*", "Excluido");

        public DocumentoStatus(string value, string displayName)
            : base(value, displayName)
        {
        }
    }
}
