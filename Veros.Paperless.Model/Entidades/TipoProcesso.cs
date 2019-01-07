namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class TipoProcesso : Entidade
    {
        public const int SolicitacaoDeCarteira = 30;
        public const int Varejo = 30;
        public const int Uniclass = 31;
        public const int Personalite = 32;
        public const int Negocio = 33;
        public const int Fcvs = 1;
        public const int Cadmut = 2;

        public virtual string Descricao { get; set; }

        public virtual int Prioridade { get; set; }

        public virtual bool CapturaParcial { get; set; }

        public virtual int Code { get; set; }
    }
}
