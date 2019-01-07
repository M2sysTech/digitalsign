namespace Veros.Paperless.Model.Servicos.Neurotech
{
    using System.Collections.Generic;

    public class ResultadoConsultaNeurotech
    {
        public string Resultado
        {
            get;
            set;
        }

        public bool Sucesso
        {
            get;
            set;
        }

        public string CodigoMensagem
        {
            get;
            set;
        }

        public long CodigoOperacao
        {
            get;
            set;
        }

        public IList<ConsultaNeurotech> ConsultasRealizadas
        {
            get;
            set;
        }

        public IList<RegrasNeurotech> RegrasAvaliadas
        {
            get;
            set;
        }

        public IList<RetornoNeurotech> Retorno
        {
            get;
            set;
        }
    }
}