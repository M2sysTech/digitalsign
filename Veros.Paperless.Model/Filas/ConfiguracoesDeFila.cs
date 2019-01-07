namespace Veros.Paperless.Model.Filas
{
    using System;
    using VerosQueues;

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
                    ItemLockedSeconds = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalSeconds),
                    PopulateMinimumIntervalSeconds = 15
                });
            }
        }
    }
}
