namespace Veros.Framework.Text
{
    public class Cpf
    {
        private readonly string cpfSemFormatacao;

        public Cpf(string cpf)
        {
            this.cpfSemFormatacao = cpf.RemoveCaracteresEspeciais();
        }

        public string SemMascara
        {
            get
            {
                return this.cpfSemFormatacao;
            }
        }

        public string ComMascara
        {
            get
            {
                if (this.cpfSemFormatacao.Length != 11)
                {
                    return this.cpfSemFormatacao;
                }

                return string.Format(
                    "{0}.{1}.{2}-{3}",
                    this.cpfSemFormatacao.Substring(0, 3),
                    this.cpfSemFormatacao.Substring(3, 3),
                    this.cpfSemFormatacao.Substring(6, 3),
                    this.cpfSemFormatacao.Substring(9, 2));
            }
        }
    }
}
