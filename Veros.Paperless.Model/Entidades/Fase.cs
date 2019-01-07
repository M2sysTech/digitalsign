namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;

    public class Fase
    {
        public static string[] TodasAsFasesPorOrdemDeExecucao = new[]
        {
            LoteStatus.AguardandoReconhecimento.Value,
            LoteStatus.AguardandoIdentificacao.Value,
            LoteStatus.AguardandoMontagem.Value,
            DocumentoStatus.ParaDigitacao.Value,
            DocumentoStatus.ParaValidacao.Value,
            DocumentoStatus.StatusParaProvaZero.Value,
            DocumentoStatus.AguardandoAprovacao.Value,
            DocumentoStatus.StatusParaAjusteOrigem.Value,
            DocumentoStatus.StatusParaExportacao.Value,
            LoteStatus.Finalizado.Value
        };

        public static IList<DocumentoStatus> FasesQueTratamDocumentos = new[]
        {
            DocumentoStatus.ParaDigitacao,
            DocumentoStatus.ParaValidacao,
            DocumentoStatus.StatusParaProvaZero,
            DocumentoStatus.AguardandoAprovacao,
            DocumentoStatus.StatusParaAjusteOrigem,
            DocumentoStatus.StatusParaExportacao
        };

        private readonly string status;

        public Fase(string status)
        {
            this.status = status;
        }

        public static string RetornarMaiorFase(params string[] keys)
        {
            int intPos = 0;
            int cont = 0;
            
            foreach (var key in keys)
            {
                if (cont > intPos)
                {
                    intPos = cont;
                }

                cont = key.IndexOf(key);    
            }

            return keys.GetValue(intPos).ToString();
        }

        public bool EhMaiorQue(string statusParaComparar)
        {
            var indiceDafaseParaComparar = Array.BinarySearch(
                Fase.TodasAsFasesPorOrdemDeExecucao,
                statusParaComparar);

            var indiceDaFaseAtual = Array.BinarySearch(
                Fase.TodasAsFasesPorOrdemDeExecucao,
                this.status);

            return indiceDaFaseAtual > indiceDafaseParaComparar;
        }
    }
}