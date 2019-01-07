namespace Veros.Paperless.Model.Servicos.RecepcaoColeta
{
    using System.Collections.Generic;

    public interface IRecepcaoDeColetaServico
    {
        void Executar(int coletaId, IList<string> caixas, IList<string> status);
    }
}
