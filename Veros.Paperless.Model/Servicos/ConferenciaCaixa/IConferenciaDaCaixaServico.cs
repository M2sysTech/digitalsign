namespace Veros.Paperless.Model.Servicos.ConferenciaCaixa
{
    using System.Collections.Generic;

    public interface IConferenciaDaCaixaServico
    {
        void Executar(int coletaId, IList<string> caixas, IList<string> status);
    }
}
