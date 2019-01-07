namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface ILiberaAprovacaoServico
    {
        void Executar(Processo processo,
            IList<RegraViolada> regrasVioladasAprovadas);
    }
}