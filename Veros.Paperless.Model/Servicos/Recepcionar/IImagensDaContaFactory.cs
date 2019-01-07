namespace Veros.Paperless.Model.Servicos.Recepcionar
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public interface IImagensDaContaFactory
    {
        IList<ImagemConta> Criar(Lote lote);
    }
}