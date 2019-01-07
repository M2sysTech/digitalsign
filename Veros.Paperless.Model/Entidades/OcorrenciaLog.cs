namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class OcorrenciaLog : Entidade
    {
        public virtual Ocorrencia Ocorrencia { get; set; }

        public virtual DateTime DataRegistro { get; set; }

        public virtual Usuario UsuarioRegistro { get; set; }

        public virtual string Observacao { get; set; }

        public virtual string Acao { get; set; }
    }
}
