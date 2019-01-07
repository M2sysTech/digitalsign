namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;

    public class ValidaNomeConjugeIncompleto : IValidaNomeConjugeIncompleto
    {
        public bool Validar(Processo processo)
        {
            var indexacaoEstadoCivilProponente = processo.ObterIndexador(TipoDocumento.CodigoFichaDeCadastro, Campo.ReferenciaDeArquivoEstadoCivilDoParticipante);

            if (indexacaoEstadoCivilProponente == null)
            {
                return false;
            }

            var listaCasados = new List<string>()
                                   {
                                       EstadoCivilDominioOriginal.Casado, 
                                       EstadoCivilDominioOriginal.CasadoComParcialBens, 
                                       EstadoCivilDominioOriginal.CasadoComTotalBens,
                                       EstadoCivilDominioOriginal.CasadoSeparacaoBens,
                                       EstadoCivilDominioOriginal.CasadoSeparacaoBensObrigatorio
                                   };

            //// se possui um estadoCivil diferente da lista acima, pode sair da regra
            if (!listaCasados.Any(x => x == indexacaoEstadoCivilProponente.SegundoValor))
            {
                return false;
            }

            //// daqui pra frente, o titular está casado. Validar dados do nome do conjuge
            var indexacaoNomeConjuge = processo.ObterIndexador(TipoDocumento.CodigoFichaDeCadastro, Campo.ReferenciaDeArquivoNomeConjuge);
            if (indexacaoNomeConjuge == null)
            {
                return true;
            }

            if (string.IsNullOrEmpty(indexacaoNomeConjuge.SegundoValor))
            {
                return true;
            }

            var arrayNomeConjuge = indexacaoNomeConjuge.SegundoValor.Trim().Split(' ');
            if (arrayNomeConjuge.Count() < 2)
            {
                //// se tem só 1 nome, é problema.
                return true;
            }

            return false;
        }
    }
}
