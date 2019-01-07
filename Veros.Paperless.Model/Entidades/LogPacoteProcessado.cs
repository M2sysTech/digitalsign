namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class LogPacoteProcessado : Entidade
    {
        public static string AcaoAprovadoNoControleDeQualidade = "AQ";
        public static string AcaoReprovadoNoControleDeQualidade = "RQ";

        public virtual Usuario Usuario
        {
            get;
            set;
        }

        public virtual PacoteProcessado PacoteProcessado
        {
            get;
            set;
        }

        public virtual string Acao
        {
            get;
            set;
        }

        public virtual string Observacao
        {
            get;
            set;
        }
    }
}
