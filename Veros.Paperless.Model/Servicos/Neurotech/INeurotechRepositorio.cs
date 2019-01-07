namespace Veros.Paperless.Model.Servicos.Neurotech
{
    using System.Collections.Generic;

    public interface INeurotechRepositorio
    {
        ResultadoConsultaNeurotech ConsultarProposta(Credencial credencial, long propostaId, IList<Parametro> parametros);
    }
}