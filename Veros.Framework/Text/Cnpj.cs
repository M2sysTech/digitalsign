namespace Veros.Framework.Text
{
    public class Cnpj
    {
        private readonly string cnpjSemFormatacao;

        public Cnpj(string cnpj)
        {
            this.cnpjSemFormatacao = cnpj.RemoveCaracteresEspeciais();
        }

        public string SemMascara
        {
            get
            {
                return this.cnpjSemFormatacao;
            }
        }

        public string ComMascara
        {
            get
            {
                if (this.cnpjSemFormatacao.Length != 14)
                {
                    return this.cnpjSemFormatacao;
                }

                return string.Format(
                    "{0}.{1}.{2}/{3}-{4}",
                    this.cnpjSemFormatacao.Substring(0, 2),
                    this.cnpjSemFormatacao.Substring(2, 3),
                    this.cnpjSemFormatacao.Substring(5, 3),
                    this.cnpjSemFormatacao.Substring(8, 4),
                    this.cnpjSemFormatacao.Substring(12, 2));
            }
        }
    }
}
