namespace Veros.Paperless.Model.Consultas
{
    using System;

    public class DataExpurgoConsulta
    {
        public string Data
        {
            get;
            set;
        }

        public DateTime ConverterParaDateTime()
        {
            return Convert.ToDateTime(this.Data);
        }
    }
}
