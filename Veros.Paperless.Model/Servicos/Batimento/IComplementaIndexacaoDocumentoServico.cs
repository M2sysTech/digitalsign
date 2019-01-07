namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Servicos.Batimento.Experimental;
    using Entidades;

    public interface IComplementaIndexacaoDocumentoServico
    {
        void Execute(Documento documento, ImagemReconhecida imagemReconhecida, ResultadoBatimentoDocumento resultadoBatimento);
    }
}