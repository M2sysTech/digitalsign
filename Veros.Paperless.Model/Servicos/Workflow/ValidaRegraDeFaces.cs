namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Collections.Generic;
    using System.Linq;
    using Entidades;
    using Repositorios;

    public class ValidaRegraDeFaces : IValidaRegraDeFaces
    {
        private readonly IComparaBioRepositorio comparaBioRepositorio;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;

        public ValidaRegraDeFaces(IComparaBioRepositorio comparaBioRepositorio,
            IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.comparaBioRepositorio = comparaBioRepositorio;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
        }

        public void Validar(Processo processo)
        {
            var regraViolada = processo.RegrasVioladas.FirstOrDefault(x => x.Regra.Id == Regra.CodigoRegraVerificacaoDeFace);

            if (regraViolada == null || regraViolada.Status != RegraVioladaStatus.Pendente)
            {
                return;
            }

            var comparacoes = this.comparaBioRepositorio.ObterTodasPorProcesso(processo.Id);

            if (comparacoes.Any(x => x.Status == "P"))
            {
                return;
            }
            
            regraViolada.Status = this.EncontrouFacesComCpfDiferentes(comparacoes) ? RegraVioladaStatus.Marcada : RegraVioladaStatus.Aprovada;

            this.regraVioladaRepositorio.AlterarStatus(regraViolada.Id, regraViolada.Status);
        }

        private bool EncontrouFacesComCpfDiferentes(IEnumerable<ComparaBio> comparacoes)
        {
            return comparacoes.Any(x => x.Resultado == "S" && x.Face1.Cpf != x.Face2.Cpf);
        }
    }
}
