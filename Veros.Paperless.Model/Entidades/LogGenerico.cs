namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class LogGenerico : Entidade
    {
        public static string AcaoRecebimento = "R";
        public static string AcaoErroRecebimento = "ER";
        public static string AcaoErroNaExportacao = "EE";
        public static string AcaoAtualizaTipoDocumento = "AT";
        public static string AcaoExcluiTipoDocumento = "ET";
        public static string AcaoMigraDossie = "MD";
        public static string AcaoCriaCaixa = "CC";
        public static string AcaoAlteracaoDeSenha = "AS";
        public static string AcaoAlteracaoDeSenhaDeOutroUsuario = "AU";
        public static string AcaoNovoTipoDocumento = "NT";
        public static string AcaoAlterarPacoteProcessado = "AP";
        public static string AcaoMarcarCaixaParaDevolucao = "CA";
        public static string AcaoMarcarCaixaComoDevolvida = "CD";
        public static string AcaoMarcarCaixaComoConferida = "CF";
        public static string AcaoInicioPreparo = "IP";
        public static string AcaoFimPreparo = "FP";
        public static string AcaoAtualizarPreparo = "UP";
        public static string AcaoAprovarLoteCef = "AL";
        public static string AcaoReprovarLoteCef = "RL";
        public static string AcaoAlteracaoDePerfil = "LP";
        public static string AcaoProcedure = "PP";

        public static string ModuloWsRecepcao = "WSRecepcao";
        public static string ModuloRecepcao = "Recepcao";
        public static string ModuloExport = "Export";
        public static string ModuloTiposDocumento = "Tipos de Documento";
        public static string ModuloMigracaoDeDossie = "Migra Dossie";
        public static string ModuloCriaCaixa = "Cria caixa na conferência";
        public static string ModuloPreparo = "Preparo";
        public static string ModuloPerfil = "Perfil de Usuários";

        public LogGenerico()
        {
            this.DataRegistro = DateTime.Now;
        }

        public virtual string Acao { get; set; }

        public virtual string Observacao { get; set; }

        public virtual DateTime DataRegistro { get; set; }

        public virtual string UsuarioNome { get; set; }

        public virtual string Modulo { get; set; }

        public virtual int Registro { get; set; }

        public virtual string EstacaoDeTrabalho { get; set; }

        public virtual string Checado { get; set; }
    }
}