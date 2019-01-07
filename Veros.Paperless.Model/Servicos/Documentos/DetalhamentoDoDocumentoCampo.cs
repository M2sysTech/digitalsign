namespace Veros.Paperless.Model.Servicos.Documentos
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;

    public class DetalhamentoDoDocumentoCampo
    {
        public int IndexacaoId { get; set; }

        public Campo Campo { get; set; }

        public string Valor1 { get; set; }

        public string Valor2 { get; set; }

        public string ValorOk { get; set; }

        public string ValorFormatado { get; set; }

        public string Valor1Formatado { get; set; }

        public string Valor2Formatado { get; set; }

        public bool EstaIlegivel
        {
            get
            {
                return this.ValorOk == "?" || (this.Valor1 == "?" && string.IsNullOrEmpty(this.ValorOk));
            }
        }

        public bool EstaInexistente
        {
            get
            {
                return this.ValorOk == "#" || (this.Valor1 == "#" && string.IsNullOrEmpty(this.ValorOk));
            }
        }

        public IList<DominioCampo> DominioDeValores { get; set; }

        public string ValorParaExibicao()
        {
            if (string.IsNullOrEmpty(this.ValorOk) || this.ValorOk == "?" || this.ValorOk == "#")
            {
                return this.Valor2Formatado;
            }

            return this.ValorFormatado;
        }

        public string ValorFinal()
        {
            if (string.IsNullOrEmpty(this.ValorOk) || this.ValorOk == "?" || this.ValorOk == "#")
            {
                return string.IsNullOrEmpty(this.Valor2) ? string.Empty : this.Valor2.Trim();
            }

            return this.ValorOk.Trim();
        }

        public bool ValoresDivergentes()
        {
            return string.IsNullOrEmpty(this.Valor2) == false && string.IsNullOrEmpty(this.ValorOk) == false && (this.Valor2.Trim() != this.ValorOk.Trim() && this.ValorOk.Trim() != "?" && this.ValorOk.Trim() != "#");
        }

        public string ValorDeCadastro()
        {
            if (string.IsNullOrEmpty(this.Valor2Formatado))
            {
                return this.Valor1;
            }

            return this.Valor2Formatado;
        }
    }
}