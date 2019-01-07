namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Veros.Framework.Modelo;

    public class ExpurgoLog : Entidade
    {
        public virtual DateTime DataHora
        {
            get;
            set;
        }

        public virtual string Mensagem
        {
            get;
            set;
        }
    }
}