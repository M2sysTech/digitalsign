namespace Veros.Paperless.Model.Entidades
{
    using System;

    public class ScannerConfigurado
    {
        public IEquatable<string> Nome
        {
            get; 
            set;
        }

        public string Identificacao
        {
            get; 
            set;
        }
    }
}