namespace Veros.Paperless.Model.Servicos.Batimento.Experimental
{
    using System.Linq;
    using Entidades;
    using Framework;

    public class ValidarResultadoBatimento : IValidarResultadoBatimento
    {
        public ResultadoBatimentoDocumento ValidarNumeroDocumentoIdentificacao(
            ResultadoBatimentoDocumento resultadoBatimento,
            Documento documento)
        {
            if (documento.Indexacao == null || 
                documento.Indexacao.Any(x => x.Campo == null) || 
                resultadoBatimento.Campos.Any(x => x.Indexacao == null || x.Indexacao.Campo == null))
            {
                return resultadoBatimento;
            }

            var valorCpf = (from valor 
                            in documento.Indexacao 
                            where valor.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoCpf 
                            select valor.SegundoValor).FirstOrDefault();

            foreach (var campoBatido in resultadoBatimento.Campos)
            {
                if (campoBatido.Indexacao.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoNumeroDocumentoIdentificacao)
                {
                    var numeroDi = campoBatido.Indexacao.SegundoValor;

                    if (valorCpf == numeroDi)
                    {
                        campoBatido.Batido = false;
                    }

                    break;
                }
            }

            return resultadoBatimento;
        }
    }
}