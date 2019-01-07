namespace Veros.Paperless.Model.ViewModel
{
    using System;

    public class LogDeAjusteViewModel
    {
        public string Tipo { get; set; }

        public int UsuarioId { get; set; }

        public DateTime? Data { get; set; }

        public string Observacao { get; set; }

        public string Acao { get; set; }
    }
}
