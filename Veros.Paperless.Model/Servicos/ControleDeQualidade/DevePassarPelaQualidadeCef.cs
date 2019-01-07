namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using System;
    using Entidades;

    public class DevePassarPelaQualidadeCef : IDevePassarPelaQualidadeCef
    {
        public bool Validar(Lote lote)
        {
            if (lote.LoteCef == null)
            {
                var message = string.Format("Lote #{0} está sem lote CEF", lote.Id);
                throw new InvalidOperationException(message);
            }

            var selecionarEmCada = Math.Truncate(100.0 / Contexto.QualidadePorcentagemCef);

            if (Contexto.ContagemDossieQualidadeCef.ContainsKey(lote.LoteCef.Id) == false)
            {
                Contexto.ContagemDossieQualidadeCef.Add(lote.LoteCef.Id, 1);
            }

            //// overhead controla quantos dossies foram marcados na Qualidade M2 e que seriam selecionados pra CEF
            if (Contexto.OverheadDossieQualidadeCef.ContainsKey(lote.LoteCef.Id) == false)
            {
                Contexto.OverheadDossieQualidadeCef.Add(lote.LoteCef.Id, 0);
            }

            //// se lote ja foi setado anteriormente, voltou pra ajuste e esta aqui de novo, não conta nada, só joga pra cef
            if (lote.QualidadeCef == 1)
            {
                return true;
            }

            var contagemLocal = Contexto.ContagemDossieQualidadeCef[lote.LoteCef.Id];

            if (contagemLocal >= selecionarEmCada)
            {
                if (lote.ProblemaQualidade == LoteMarcaQualidade.PossuiProblemaQualidade)
                {
                    Contexto.OverheadDossieQualidadeCef[lote.LoteCef.Id]++;
                    return false;
                }

                if (lote.QualidadeM2sys == 0)
                {
                    Contexto.OverheadDossieQualidadeCef[lote.LoteCef.Id]++;
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}