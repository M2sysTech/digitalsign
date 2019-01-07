namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class ConfiguracaoDeTipoDeProcesso : Entidade
    {
        public virtual TipoProcesso TipoProcesso { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }

        public virtual int QuantidadeDePaginas { get; set; }

        public virtual string Obrigatorio { get; set; }
    }
}