namespace Veros.Paperless.Model.Entidades
{
    using Framework.Modelo;

    public class DossieContingencia : Entidade
    {
        public virtual string MatriculaAgente { get; set; }

        public virtual string NumeroContrato { get; set; }

        public virtual string Hipoteca { get; set; }

        public virtual string NomeDoMutuario { get; set; }

        public virtual string CodigoDeBarras { get; set; }

        public virtual string UfArquivo { get; set; }

        public virtual string Situacao { get; set; }

        public virtual string CaixaEtiqueta { get; set; }

        public virtual string CaixaSequencial { get; set; }

        public virtual DossieEsperado ConverteEmDossieEsperado()
        {
            var dossieEsperado = new DossieEsperado
            {
                CodigoDeBarras = this.CodigoDeBarras,
                Hipoteca = this.Hipoteca,
                MatriculaAgente = this.MatriculaAgente,
                NomeDoMutuario = this.NomeDoMutuario,
                NumeroContrato = this.NumeroContrato,
                Situacao = this.Situacao,
                UfArquivo = this.UfArquivo
            };

            return dossieEsperado;
        }
    }
}
