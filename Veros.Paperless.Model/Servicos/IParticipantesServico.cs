namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;
    using Entidades;

    public interface IParticipantesServico
    {
        IList<Participante> Obter(Processo processo);
        
        IList<Participante> ObterComRegrasMarcadas(Processo processo);
    }
}