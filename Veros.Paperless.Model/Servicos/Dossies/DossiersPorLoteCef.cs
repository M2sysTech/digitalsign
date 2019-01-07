namespace Veros.Paperless.Model.Servicos.Dossies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModel;

    public class DossiersPorLoteCef
    {
        public string Linhas
        {
            get;
            set;
        }

        public static DossiersPorLoteCef Criar(IList<DossieStatusViewModel> dossiesPorLoteCef)
        {
            var conteudoArquivo = string.Format(
               "Arquivo gerado em {0}",
               DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")) + Environment.NewLine;

            conteudoArquivo += "Coleta;LoteCEF.Code;LoteCEFData;Num.Proc.M2sys;TipoProcesso;MatriculaAF;Contrato;GrauHipoteca;Num.Processo;Folder;Caixa;Situação;DataSituação;DataLoteCef;DataAprovaçãoCef;DataInicial;QtdePaginas";

            var linhasAtuais = dossiesPorLoteCef.Aggregate(
                string.Empty, 
                (current, item) => 
                    current + Environment.NewLine + string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16}", item.ColetaCode, item.LoteCefCode, item.LoteCefDataFim, item.ProcessoId, item.TipoProcessoDesc(), item.MatriculAgente(), item.ContratoNumero(), item.HipotecaGrau(), item.NumeroProcessoMontado(), item.FolderDossie, item.CodCaixa, item.Situacao(), item.DataSituacao(), item.DataLoteCef(), item.DataLoteCefAprovacao(), ((DateTime)item.CaixaData).ToString("dd/MM/yyyy"), item.QtdePaginas));

            conteudoArquivo += linhasAtuais;

            return new DossiersPorLoteCef
            {
                Linhas = conteudoArquivo
            };
        }
    }
}