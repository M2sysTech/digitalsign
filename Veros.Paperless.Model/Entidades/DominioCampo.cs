namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class DominioCampo : Entidade
    {
        public const string CodigoDeRg = "DOMINIO_RG";

        public const string ValorEstadoCivilCasado = "1";
        public const string ValorEstadoCivilSolteiro = "4";
        public const string ValorEstadoCivilSeparado = "5";
        public const string ValorEstadoCivilViuvo = "6";
        public const string ValorEstadoCivilMaritalOuCompanheiro = "7";
        public const string ValorEstadoCivilDesquitado = "8";
        public const string ValorEstadoCivilDivorciado = "9";

        public virtual string Codigo
        {
            get;
            set;
        }

        public virtual string Chave
        {
            get;
            set;
        }

        public virtual string Descricao
        {
            get;
            set;
        }

        public static bool EstadoCivilObrigaConjuge(string estadoCivil)
        {
            return estadoCivil == DominioCampo.ValorEstadoCivilCasado || estadoCivil == DominioCampo.ValorEstadoCivilMaritalOuCompanheiro;
        }

        public static bool EstadoCivilProibeConjuge(string estadoCivil)
        {
            return estadoCivil == DominioCampo.ValorEstadoCivilSolteiro;
        }
    }
}
