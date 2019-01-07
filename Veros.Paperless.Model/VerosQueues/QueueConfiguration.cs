//-----------------------------------------------------------------------
// <copyright file="QueueConfiguration.cs" company="Veros IT">
//      Copyright (c) Veros Tecnologia da Informação Ltda
//      Todos os direitos reservados
// </copyright>
//-----------------------------------------------------------------------

namespace Veros.Paperless.Model.VerosQueues
{
    public class QueueConfiguration
    {
        public QueueConfiguration()
        {
            this.PopulateMinimumIntervalSeconds = 30;
            this.CheckLockedItems = true;
            this.ItemLockedSeconds = 60;
            this.MaximumItems = 1000;
            this.VersaoRecognizeServerAutorizada = string.Empty;
        }

        public int PopulateMinimumIntervalSeconds
        {
            get;
            set;
        }

        public int ItemLockedSeconds
        {
            get;
            set;
        }

        public int MaximumItems
        {
            get;
            set;
        }

        public bool CheckLockedItems
        {
            get;
            set;
        }

        public string VersaoRecognizeServerAutorizada
        {
            get; 
            set;
        }
    }
}