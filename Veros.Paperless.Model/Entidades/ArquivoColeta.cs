namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Modelo;

    public class ArquivoColeta : Entidade
    {
        public const string AguardandoAnalise = "A";
        public const string PendenciasDetectadas = "P";
        public const string SemPendencias = "S";
        public const string Descartado = "D";

        public ArquivoColeta()
        {
            this.Pendencias = new List<PendenciaColeta>();
        }

        public virtual DateTime DataUpado { get; set; }

        public virtual string Status { get; set; }

        public virtual string NomeArquivo { get; set; }

        public virtual Usuario UsuarioUpado { get; set; }

        public virtual int TamanhoBytes { get; set; }

        public virtual Coleta Coleta { get; set; }

        public virtual IList<PendenciaColeta> Pendencias { get; set; }

        public virtual int QuantidadeDeCaixas { get; set; }

        public virtual int QuantidadeDeCaixasDuplicadas { get; set; }

        public virtual int QuantidadeDeDossies { get; set; }

        public virtual bool PossuiPendencia()
        {
            if (this.Pendencias == null)
            {
                return false;
            }

            return this.Pendencias.Any(x => string.IsNullOrEmpty(x.Texto));
        }
    }
}
