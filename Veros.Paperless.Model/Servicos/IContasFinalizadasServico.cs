namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;

    public interface IContasFinalizadasServico
    {
        IEnumerable<dynamic> Obter();

        bool Existe();
    }
}