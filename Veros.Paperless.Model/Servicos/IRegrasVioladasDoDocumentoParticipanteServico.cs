namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;
    using Entidades;

    public interface IRegrasVioladasDoDocumentoParticipanteServico
    {
        IList<RegraVioladaDocumentoParticipante> Obter(Processo processo, Participante participante); 
    }
}