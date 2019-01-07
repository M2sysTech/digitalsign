namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class Face : Entidade
    { 
        public virtual Pagina Pagina { get; set; }

        public virtual string TipoDeArquivo { get; set; }

        public virtual FaceStatus StatusDaComparacao { get; set; }

        public virtual string Cpf { get; set; }

        public virtual string Comum { get; set; }

        public virtual string ListaNegra { get; set; }

        public virtual string NomeArquivo { get; set; }

        public virtual int QuantidadeFaceComum { get; set; }
    }
}
