namespace Veros.Paperless.Infra.Filas
{
    using System;
    using Veros.Queues.Server.Configuration;

    public class ConfiguracoesDeFila
    {
        private static QueueConfiguration ocr;

        public static QueueConfiguration Ocr
        {
            get
            {
                return ocr ?? (ocr = new QueueConfiguration
                {
                    MaximumItems = 300,
                    ItemLockedSeconds = Convert.ToInt32(TimeSpan.FromMinutes(1).TotalSeconds),
                    PopulateMinimumIntervalSeconds = 5
                });
            }
        }
    }
}
