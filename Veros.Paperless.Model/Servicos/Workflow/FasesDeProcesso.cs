namespace Veros.Paperless.Model.Servicos.Workflow
{
    using Entidades;
    using Framework;

    public class FasesDeProcesso : IFasesDeProcesso
    {
        public IFaseDeWorkflow<Processo, ProcessoStatus>[] Obter()
        {
            return new IFaseDeWorkflow<Processo, ProcessoStatus>[]
            {
                IoC.Current.Resolve<FaseProcessoPdfMontado>(),

                IoC.Current.Resolve<FaseProcessoAguardandoMontagem>(),
                IoC.Current.Resolve<FaseProcessoMontado>(),
                IoC.Current.Resolve<FaseProcessoSetaDigitacao>(),

                IoC.Current.Resolve<FaseProcessoAguardandoDigitacao>(),
                IoC.Current.Resolve<FaseProcessoDigitado>(),

                IoC.Current.Resolve<FaseProcessoSetaConsulta>(),
                IoC.Current.Resolve<FaseProcessoAguardandoConsulta>(),
                IoC.Current.Resolve<FaseProcessoConsultado>(),
                
                IoC.Current.Resolve<FaseProcessoSetaValidacao>(),
                IoC.Current.Resolve<FaseProcessoAguardandoValidacao>(),
                IoC.Current.Resolve<FaseProcessoValidado>(),

                IoC.Current.Resolve<FaseProcessoSetaProvaZero>(),
                IoC.Current.Resolve<FaseProcessoAguardandoProvaZero>(),
                IoC.Current.Resolve<FaseProcessoProvaZeroRealizada>(),

                IoC.Current.Resolve<FaseProcessoSetaFormalistica>(),
                IoC.Current.Resolve<FaseProcessoAguardandoFormalistica>(),
                IoC.Current.Resolve<FaseProcessoFormalisticaRealizada>(),

                IoC.Current.Resolve<FaseProcessoSetaRevisao>(),
                IoC.Current.Resolve<FaseProcessoAguardandoRevisao>(),
                IoC.Current.Resolve<FaseProcessoRevisaoRealizada>(),

                IoC.Current.Resolve<FaseProcessoSetaAprovacao>(),
                IoC.Current.Resolve<FaseProcessoAprovado>(),
                
                IoC.Current.Resolve<FaseProcessoSetaExportacao>(),
                IoC.Current.Resolve<FaseProcessoExportado>(),

                IoC.Current.Resolve<FaseProcessoSetaEnvio>(),
                IoC.Current.Resolve<FaseProcessoEnviado>(),

                IoC.Current.Resolve<FaseProcessoSetaRetorno>(),
                IoC.Current.Resolve<FaseProcessoRetornoFinalizado>(),

                IoC.Current.Resolve<FaseProcessoFinalizado>(),
                IoC.Current.Resolve<FaseProcessoSetaErro>(),
                IoC.Current.Resolve<FaseProcessoErro>()
            };
        }
    }
}