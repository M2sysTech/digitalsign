namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using Entidades;

    public interface IObtemOpcoesDeValorDeIndexacaoServico
    {
        IList<DominioCampo> Obter(Indexacao indexacao);
    }
}