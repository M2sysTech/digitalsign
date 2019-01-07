namespace Veros.Paperless.Model.Servicos.Workflow
{
    using System.Linq;
    using EngineDeRegras;
    using Entidades;
    using Repositorios;

    public class ValidaRegraDeListaNegra : IValidaRegraDeListaNegra
    {
        private readonly IRegraRepositorio regraRepositorio;
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        private readonly IListaNegraCpfRepositorio listaNegraCpfRepositorio;
        private readonly ISalvaRegraViolada salvaRegraViolada;

        public ValidaRegraDeListaNegra(IRegraRepositorio regraRepositorio,
            IRegraVioladaRepositorio regraVioladaRepositorio,
            IListaNegraCpfRepositorio listaNegraCpfRepositorio,
            ISalvaRegraViolada salvaRegraViolada)
        {
            this.regraRepositorio = regraRepositorio;
            this.regraVioladaRepositorio = regraVioladaRepositorio;
            this.listaNegraCpfRepositorio = listaNegraCpfRepositorio;
            this.salvaRegraViolada = salvaRegraViolada;
        }

        public bool Validar(Processo processo)
        {
            var regra = this.ObterRegraParaValidacao(processo);

            if (regra == null)
            {
                return false;
            }

            var cpf = this.ObterCpf(processo);

            if (string.IsNullOrEmpty(cpf) || this.listaNegraCpfRepositorio.CpfEstaNaListaNegra(cpf) == false)
            {
                return false;
            }

            var regraViolada = new RegraViolada
            {
                Regra = regra,
                Processo = processo,
                Observacao = regra.Descricao,
                Status = RegraVioladaStatus.Marcada
            };

            this.salvaRegraViolada.Salvar(processo, regraViolada, Regra.FaseAprovacao);
            return true;
        }

        private string ObterCpf(Processo processo)
        {
            var documentoDeIdentificacao = processo.Documentos.FirstOrDefault(x => x.TipoDocumento.IsDi);

            if (documentoDeIdentificacao == null)
            {
                return string.Empty;
            }

            return documentoDeIdentificacao.ObterCpf();
        }

        private Regra ObterRegraParaValidacao(Processo processo)
        {
             var regra = this.regraRepositorio.ObterPorId(432);

             if (regra == null || regra.Ativada != "S")
             {
                 return null;
             }

             if (this.regraVioladaRepositorio.ExisteRegraVioladaPorRegra(processo.Id, regra.Id))
             {
                 return null;
             }

            return regra;
        }
    }
}
