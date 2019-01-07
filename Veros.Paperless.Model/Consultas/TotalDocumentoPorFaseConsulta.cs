namespace Veros.Paperless.Model.Consultas
{
    public class TotalDocumentoPorFaseConsulta
    {
        public virtual string Status
        {
            get;
            set;
        }

        public virtual long QuantidadeDocumentos
        {
            get;
            set;
        }

        public virtual string NomeFase
        {
            get
            {
                return this.RetornaDescricaoFasePorStatus(this.Status);
            }
        }

        public string RetornaDescricaoFasePorStatus(string status)
        {
            switch (status)
            {
                case "55":
                    return "Ocr";
                case "62":
                    return "Identificação";
                case "65":
                    return "Montagem";
                case "75":
                    return "Digitação";
                case "95":
                    return "Validação";
                case "A5":
                    return "ProvaZero";
                case "C5":
                    return "Aprovação";
                case "E5":
                    return "Ajuste Origem";
                case "F5":
                    return "Exportação";
                case "G0":
                    return "Fechado";
                default:
                    return this.Status;
            }
        }
    }
}