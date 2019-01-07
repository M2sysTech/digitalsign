namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class RelacionamentoDeCurso : Entidade
    {
        public virtual EnderecoDeInstituicaoDeEnsino EnderecoDeInstituicaoDeEnsino
        {
            get;
            set;
        }

        public virtual Curso Curso
        {
            get;
            set;
        }
    }
}