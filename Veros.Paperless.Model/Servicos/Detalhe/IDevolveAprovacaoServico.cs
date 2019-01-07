namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface IDevolveAprovacaoServico
    {
        void Executar(Processo processo,
            IList<RegraViolada> regrasVioladasMarcadas,
            IList<RegraViolada> regrasVioladasAprovadas,
            string parecerDoBanco);
    }
}