namespace Veros.Paperless.Model.Consultas
{
    using System;
    using System.Collections.Generic;

    public interface IMassaTesteConfigPalavrasChavesConsulta
    {
        IList<DocumentoParaPalavraChave> Obter(string sqlNativo);
    }
}