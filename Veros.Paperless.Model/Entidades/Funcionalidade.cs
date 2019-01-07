namespace Veros.Paperless.Model.Entidades
{
    using System.Collections.Generic;
    using Framework;

    public class Funcionalidade : EnumerationString<Funcionalidade>
    {
        public static readonly Funcionalidade Captura = new Funcionalidade("1", "Realizar Captura");
        public static readonly Funcionalidade SolicitacaoNovaSenha = new Funcionalidade("2", "Solicitar Nova Senha");
        public static readonly Funcionalidade AlterarSenhaAcesso = new Funcionalidade("3", "Alterar Senha");
        public static readonly Funcionalidade AdministrativoUsuario = new Funcionalidade("4", "Administração de Usuário");
        public static readonly Funcionalidade AdministrativoPerfil = new Funcionalidade("5", "Administração de Perfil");
        public static readonly Funcionalidade Dashboard = new Funcionalidade("6", "Acessar Dashboad");
        public static readonly Funcionalidade PesquisaDeProcessos = new Funcionalidade("7", "Visualizar Processo");
        public static readonly Funcionalidade CadastroDeColeta = new Funcionalidade("8", "Cadastro de Coleta");
        public static readonly Funcionalidade RealizacaoDeColeta = new Funcionalidade("9", "Realização de Coleta");
        public static readonly Funcionalidade RelatorioDevolucao = new Funcionalidade("10", "Relatório de Devolução");
        public static readonly Funcionalidade Preparo = new Funcionalidade("11", "Preparo");
        public static readonly Funcionalidade RelatorioProdutividade = new Funcionalidade("12", "Relatório de Produtividade");
        public static readonly Funcionalidade RelatorioDeCaixasEmConferencia = new Funcionalidade("13", "Relatório de Caixas em Conferência");
        public static readonly Funcionalidade Classificacao = new Funcionalidade("14", "Formalística");
        public static readonly Funcionalidade TipoDocumentoAdministrador = new Funcionalidade("15", "Tipos de Documentos - Administrador");
        public static readonly Funcionalidade Recepcao = new Funcionalidade("16", "Recepção");
        public static readonly Funcionalidade Separador = new Funcionalidade("17", "Separador");
        public static readonly Funcionalidade Conferencia = new Funcionalidade("18", "Conferência");
        public static readonly Funcionalidade AcompanhamentoOcorrencia = new Funcionalidade("19", "Acompanhamento de Ocorrências");
        public static readonly Funcionalidade RelatorioDeCaixasETotalDeDossies = new Funcionalidade("20", "Relatório de Caixas e Total de Dossiês");
        public static readonly Funcionalidade GerarFolhaDePreparo = new Funcionalidade("21", "Gerar Folha de Preparo");
        public static readonly Funcionalidade ControleQualidadeCef = new Funcionalidade("22", "Controle de Qualidade CEF");
        public static readonly Funcionalidade ControleQualidadeM2 = new Funcionalidade("23", "Controle de Qualidade M2");
        public static readonly Funcionalidade RelatorioDeCaixasPorUniColRecArm = new Funcionalidade("24", "Relatório de Caixas por Unidade, Coleta, Recepção e Armazenamento");
        public static readonly Funcionalidade Ajustes = new Funcionalidade("25", "Ajustes");
        public static readonly Funcionalidade RelatorioDosIndPrepDigPrev = new Funcionalidade("26", "Relatório de Dossiês Indexados, Preparados Para Digitalização e Previstos");
        public static readonly Funcionalidade Transportadora = new Funcionalidade("27", "Cadastro de Transportadora");
        public static readonly Funcionalidade AnaliseAgendamentoDeColeta = new Funcionalidade("28", "Análise de Agendamento de Coleta");        
        public static readonly Funcionalidade SolicitarColeta = new Funcionalidade("29", "Solicitar Coleta");
        public static readonly Funcionalidade AnaliseDeDevolucaoDeCaixas = new Funcionalidade("30", "Análise de Devolução de Caixas");
        public static readonly Funcionalidade PainelDeAcompanhemento = new Funcionalidade("31", "Painel de Acompanhamento");
        public static readonly Funcionalidade RelatorioFaturamentoAnalitico = new Funcionalidade("32", "Relatorio de Faturamento Analítico");
        public static readonly Funcionalidade ConfiguracaoQualidade = new Funcionalidade("33", "Configuração Controle de Qualidade");
        public static readonly Funcionalidade TipoDocumentoLeitor = new Funcionalidade("34", "Tipos de Documentos - Leitor");
        public static readonly Funcionalidade TipoDocumentoEditor = new Funcionalidade("35", "Tipos de Documentos - Editor");
        public static readonly Funcionalidade CertificadoDeEntrega = new Funcionalidade("36", "Certificado de Entrega");
        public static readonly Funcionalidade ConfiguracaoDeMovimento = new Funcionalidade("37", "Configuração de Movimento");
        public static readonly Funcionalidade PortalSuporte = new Funcionalidade("38", "Portal de Suporte");
        public static readonly Funcionalidade TriagemPreOcr = new Funcionalidade("39", "Triagem Pre-OCR");
        public static readonly Funcionalidade EstoqueFilas = new Funcionalidade("40", "Estoque nas Filas");
        public static readonly Funcionalidade DevolucaoDeCaixas = new Funcionalidade("41", "Devolução de Caixas");
        public static readonly Funcionalidade ConferenciaDeDevolucaoDeCaixas = new Funcionalidade("42", "Conferência de Devolução de Caixas");
        public static readonly Funcionalidade Logs = new Funcionalidade("43", "Visualizar logs do lote");
        public static readonly Funcionalidade RelatorioDeQuantitativoDeAssinaturaDigital = new Funcionalidade("44", "Relatório de Quantitativo de Assinatura Digital");
        public static readonly Funcionalidade AjustesEspeciais = new Funcionalidade("45", "Ajustes Especiais");
        public static readonly Funcionalidade RelatorioDeDossiesNaQualidadeCef = new Funcionalidade("46", "Relatório de Dossiês na Qualidade CEF");
        public static readonly Funcionalidade RelatorioProdutividadeOperadores = new Funcionalidade("47", "Relatório de Produtividade de Operadores");
        public static readonly Funcionalidade ConfiguracaoLoteCef = new Funcionalidade("48", "Configuração de Lote CEF");
        public static readonly Funcionalidade RelatorioArquivoDossiesComStatus = new Funcionalidade("49", "Arquivo CSV Status dos Dossiês");
        public static readonly Funcionalidade ManipularArquivosDigitais = new Funcionalidade("50", "Manipular Arquivos Adicionais nos Dossiês Finalizados");
        public static readonly Funcionalidade IdentificacaoManual = new Funcionalidade("51", "Identificação Manual");
        public static readonly Funcionalidade Priorizacao = new Funcionalidade("52", "Priorização");
        public static readonly Funcionalidade AdicionarDossieNaConferencia = new Funcionalidade("53", "Botão para adicionar dossiê na tela de conferência");
        public static readonly Funcionalidade ExcluirOcorrencia = new Funcionalidade("54", "Botão para excluir ocorrência cadastrada");

        public Funcionalidade(string value, string displayName) : base(value, displayName)
        {
        }

        public static IList<Funcionalidade> Ativos()
        {
            var funcionalidades = new List<Funcionalidade>
            {
                Funcionalidade.Captura,
                Funcionalidade.SolicitacaoNovaSenha,
                Funcionalidade.AlterarSenhaAcesso,
                Funcionalidade.AdministrativoPerfil,
                Funcionalidade.AdministrativoUsuario,
                Funcionalidade.Dashboard,
                Funcionalidade.PesquisaDeProcessos,
                Funcionalidade.CadastroDeColeta,
                Funcionalidade.RealizacaoDeColeta,
                Funcionalidade.Preparo,
                Funcionalidade.Classificacao,
                Funcionalidade.TipoDocumentoAdministrador,
                Funcionalidade.Recepcao,
                Funcionalidade.Separador,
                Funcionalidade.Conferencia,
                Funcionalidade.AcompanhamentoOcorrencia,
                Funcionalidade.ControleQualidadeM2,
                Funcionalidade.ControleQualidadeCef,
                Funcionalidade.AcompanhamentoOcorrencia,
                Funcionalidade.GerarFolhaDePreparo,
                Funcionalidade.Ajustes,
                Funcionalidade.Transportadora,
                Funcionalidade.AnaliseAgendamentoDeColeta,
                Funcionalidade.SolicitarColeta,
                Funcionalidade.AnaliseDeDevolucaoDeCaixas,
                Funcionalidade.DevolucaoDeCaixas,
                Funcionalidade.ConferenciaDeDevolucaoDeCaixas,
                Funcionalidade.PainelDeAcompanhemento,
                Funcionalidade.ConfiguracaoQualidade,
                Funcionalidade.TipoDocumentoLeitor,
                Funcionalidade.TipoDocumentoEditor,
                Funcionalidade.CertificadoDeEntrega,
                Funcionalidade.ConfiguracaoDeMovimento,
                Funcionalidade.PortalSuporte,
                Funcionalidade.TriagemPreOcr,
                Funcionalidade.EstoqueFilas,
                Funcionalidade.Logs,
                Funcionalidade.AjustesEspeciais,
                Funcionalidade.ConfiguracaoLoteCef,
                Funcionalidade.RelatorioArquivoDossiesComStatus, 
                Funcionalidade.ManipularArquivosDigitais,
                Funcionalidade.IdentificacaoManual,
                Funcionalidade.Priorizacao,
                Funcionalidade.AdicionarDossieNaConferencia,
                Funcionalidade.ExcluirOcorrencia
            };

            funcionalidades.AddRange(Funcionalidade.Relatorios());

            return funcionalidades;
        }

        public static IList<Funcionalidade> Relatorios()
        {
            return new List<Funcionalidade>
            {
                Funcionalidade.RelatorioDevolucao,
                Funcionalidade.RelatorioProdutividade,                
                Funcionalidade.RelatorioDeCaixasEmConferencia,
                Funcionalidade.RelatorioDeCaixasETotalDeDossies,
                Funcionalidade.RelatorioDeCaixasPorUniColRecArm,
                Funcionalidade.RelatorioDosIndPrepDigPrev,
                Funcionalidade.RelatorioFaturamentoAnalitico,
                Funcionalidade.RelatorioDeQuantitativoDeAssinaturaDigital,
                Funcionalidade.RelatorioDeDossiesNaQualidadeCef,
                Funcionalidade.RelatorioProdutividadeOperadores,
                Funcionalidade.RelatorioArquivoDossiesComStatus
            };
        }

        public static IList<Funcionalidade> Configuracoes()
        {
            return new List<Funcionalidade>
            {
                Funcionalidade.AdministrativoPerfil,
                Funcionalidade.AdministrativoUsuario,
                Funcionalidade.ConfiguracaoDeMovimento,
                Funcionalidade.ConfiguracaoLoteCef
            };
        }
    }
}
