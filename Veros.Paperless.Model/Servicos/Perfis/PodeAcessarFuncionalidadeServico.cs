namespace Veros.Paperless.Model.Servicos.Perfis
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Entidades;

    public class ObtemRelatoriosPermitidosServico : IObtemRelatoriosPermitidosServico
    {
        private readonly IPodeAcessarFuncionalidadeServico podeAcessarFuncionalidadeServico;

        public ObtemRelatoriosPermitidosServico(IPodeAcessarFuncionalidadeServico podeAcessarFuncionalidadeServico)
        {
            this.podeAcessarFuncionalidadeServico = podeAcessarFuncionalidadeServico;
        }

        public IList<Funcionalidade> Executar()
        {
            return Funcionalidade.Relatorios()
                .Where(relatorio => this.podeAcessarFuncionalidadeServico.Executar(relatorio))
                .OrderBy(x => x.DisplayName)
                .ToList();
        }
    }
}