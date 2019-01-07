namespace Veros.Paperless.Model.Consultas
{
    public class FaturamentoPorPeriodo
    {
        public int IdPacote
        {
            get;
            set;
        }

        public string DataDoPacote
        {
            get;
            set;
        }

        public string HoraRecepcaoDoPacote
        {
            get;
            set;
        }

        public string HoraImportacaoDoPacote
        {
            get;
            set;
        }

        public int TotalRecepcionadas
        {
            get;
            set;
        }

        public int SlaOk2Horas
        {
            get;
            set;
        }

        public int SlaOk19Horas
        {
            get;
            set;
        }

        public int SlaOkApos17Horas
        {
            get;
            set;
        }

        public int ForaDoSla
        {
            get;
            set;
        }

        public int Erro
        {
            get;
            set;
        }

        public int ReconhecimentoAutomatico
        {
            get;
            set;
        }

        public int AnaliseManual
        {
            get;
            set;
        }

        public int AlteracaoCadastral
        {
            get;
            set;
        }

        public int AtuacaoBanco
        {
            get;
            set;
        }

        public int LiberadasDiretamente
        {
            get;
            set;
        }

        public int Devolvidas
        {
            get;
            set;
        }

        public int Fraudes
        {
            get;
            set;
        }

        public double PercentualSlaOk2Horas()
        {
            return this.CalculoPercentual(this.SlaOk2Horas);
        }

        public double PercentualSlaOk19Horas()
        {
            return this.CalculoPercentual(this.SlaOk19Horas);
        }

        public double PercentualSlaOkApos17Horas()
        {
            return this.CalculoPercentual(this.SlaOkApos17Horas);
        }

        public double PercentualForaSla()
        {
            return this.CalculoPercentual(this.ForaDoSla);
        }

        public double PercentualErro()
        {
            return this.CalculoPercentual(this.Erro);
        }

        public double PercentualReconhecimentoAutomatico()
        {
            return this.CalculoPercentual(this.ReconhecimentoAutomatico);
        }

        public double PercentualAnaliseManual()
        {
            return this.CalculoPercentual(this.AnaliseManual);
        }

        public double PercentualAlteracaoCadastral()
        {
            return this.CalculoPercentual(this.AlteracaoCadastral);
        }

        public double PercentualAtuacaoBanco()
        {
            return this.CalculoPercentual(this.AtuacaoBanco);
        }

        public double PercentualLiberadasDiretamente()
        {
            return this.CalculoPercentual(this.LiberadasDiretamente);
        }

        public double PercentualDevolvidas()
        {
            return this.CalculoPercentual(this.Devolvidas);
        }

        public double PercentualFraude()
        {
            return this.CalculoPercentual(this.Fraudes);
        }

        private double CalculoPercentual(int campo)
        {
            return campo == 0.0 ? 0 : (campo / this.TotalRecepcionadas) * 100;
        }
    }
}