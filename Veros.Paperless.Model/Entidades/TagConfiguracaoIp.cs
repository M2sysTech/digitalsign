namespace Veros.Paperless.Model.Entidades
{
    using Framework;

    public class TagConfiguracaoIp : EnumerationString<TagConfiguracaoIp>
    {
        public static readonly TagConfiguracaoIp Fila = new TagConfiguracaoIp("QUEUE", "Fila");
        public static readonly TagConfiguracaoIp FilaOcr = new TagConfiguracaoIp("QUEUEOCR", "Fila de Ocr");
        public static readonly TagConfiguracaoIp FileTransfer = new TagConfiguracaoIp("FILE", "FileTransfer");
        public static readonly TagConfiguracaoIp MonitorOcr = new TagConfiguracaoIp("MONOCR", "MonitorOcr");
        public static readonly TagConfiguracaoIp Cep = new TagConfiguracaoIp("CONSULTA_CEP", "Servidor de Cep");
        public static readonly TagConfiguracaoIp Redis = new TagConfiguracaoIp("REDIS", "Redis");
        public static readonly TagConfiguracaoIp MonitorServicos = new TagConfiguracaoIp("MONSRV", "Monitor de Serviços");
        public static readonly TagConfiguracaoIp SqlProcessor = new TagConfiguracaoIp("SQLP", "Sql Processor");
        public static readonly TagConfiguracaoIp Batimento = new TagConfiguracaoIp("BATIMENTO", "Servico de Batimento");
        public static readonly TagConfiguracaoIp Export = new TagConfiguracaoIp("EXPORT", "Servico de Exportacao");
        public static readonly TagConfiguracaoIp Expurgo = new TagConfiguracaoIp("EXPURGO", "Servico de Expurgo");
        public static readonly TagConfiguracaoIp Faturamento = new TagConfiguracaoIp("FATURAMENTO", "Servico de Faturamento");
        public static readonly TagConfiguracaoIp Importacao = new TagConfiguracaoIp("IMPORT", "Servico de Importacao");
        public static readonly TagConfiguracaoIp Montagem = new TagConfiguracaoIp("MONTAGEM", "Servico de Montagem");
        public static readonly TagConfiguracaoIp Recepcao = new TagConfiguracaoIp("RECEPCAO", "Servico de Recepcao");
        public static readonly TagConfiguracaoIp Validacao = new TagConfiguracaoIp("VALIDACAO", "Servico de Validacao");
        public static readonly TagConfiguracaoIp Workflow = new TagConfiguracaoIp("WORKFLOW", "Servico de Workflow");
        public static readonly TagConfiguracaoIp Complementacao = new TagConfiguracaoIp("COMPLEMENTACAO", "Servico de Complementacao");
        public static readonly TagConfiguracaoIp Classifier = new TagConfiguracaoIp("CLASSIFIER", "Servico de Classifier");
        public static readonly TagConfiguracaoIp Directory = new TagConfiguracaoIp("DIRECTORY", "Servico de Diretorios Inbox e OutBox para OCR");
        public static readonly TagConfiguracaoIp Ajuste = new TagConfiguracaoIp("AJUSTE", "Servico de Diretorios Inbox e OutBox para OCR");
        public static readonly TagConfiguracaoIp Sender = new TagConfiguracaoIp("SENDER", "Servico de Sender");

        public TagConfiguracaoIp(string value, string counterName) : base(value, counterName)
        {
        }
    }
}