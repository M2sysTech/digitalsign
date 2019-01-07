namespace Veros.Paperless.Model.Servicos.ControleDeQualidade
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Consultas;
    using Perfis;
    using ViewModel;

    public class ObtemControleQualidadeCefViewModelServico : IObtemControleQualidadeCefViewModelServico
    {
        private readonly ITotaisAguardandoQualidadeCefConsulta totaisAguardandoQualidadeCefConsulta;
        private readonly IPodeAcessarFuncionalidadeServico podeAcessarFuncionalidadeServico;

        public ObtemControleQualidadeCefViewModelServico(ITotaisAguardandoQualidadeCefConsulta totaisAguardandoQualidadeCefConsulta,
            IPodeAcessarFuncionalidadeServico podeAcessarFuncionalidadeServico)
        {
            this.totaisAguardandoQualidadeCefConsulta = totaisAguardandoQualidadeCefConsulta;
            this.podeAcessarFuncionalidadeServico = podeAcessarFuncionalidadeServico;
        }

        public IList<ProcessamentoQualidadeCefViewModel> Executar(int pacoteProcessadoId = 0)
        {
            var podeacessar = this.podeAcessarFuncionalidadeServico.ChecarSiglaPerfil(new List<string>() { "MGTR", "CEFAD" });

            var listaViewModel = new List<ProcessamentoQualidadeCefViewModel>();

            var totaisAguardandoQualidadeCef = this.totaisAguardandoQualidadeCefConsulta.Obter(pacoteProcessadoId);

            foreach (var loteCefId in totaisAguardandoQualidadeCef.Select(x => x.LoteCefId).Distinct().ToList())
            {
                var novoitem = this.CriarItemViewModel(loteCefId, totaisAguardandoQualidadeCef);
                novoitem.PermissaoAprovacao = podeacessar;
                listaViewModel.Add(novoitem);
            }

            return listaViewModel;
        }

        private ProcessamentoQualidadeCefViewModel CriarItemViewModel(int loteCefId, IEnumerable<TotaisAguardandoQualidadeCef> totaisAguardandoQualidadeCef)
        {
            var lista = totaisAguardandoQualidadeCef.Where(x => x.LoteCefId == loteCefId).ToList();

            var item = new ProcessamentoQualidadeCefViewModel
            {
                LoteCefId = loteCefId,
                LoteCefData = lista.FirstOrDefault().LoteCefData,
                DataAprovacao = lista.FirstOrDefault().LoteCefAprovacao,
                DataAssinatura = lista.FirstOrDefault().LoteCefAssinatura,
                Status = lista.FirstOrDefault().LoteCefStatus,
                QuantidadeDeDossies = lista.Sum(x => x.TotalPacote),
                Detalhes = this.CriarDetalhePacote(lista)
            };

            return item;
        }

        private IList<DetalheProcessamentoQualidadeCefViewModel> CriarDetalhePacote(IEnumerable<TotaisAguardandoQualidadeCef> listaDoLoteCef)
        {
            var detalhes = new List<DetalheProcessamentoQualidadeCefViewModel>();

            foreach (var tipoDeAmostra in listaDoLoteCef.Where(x => x.TipoDeAmostra > 0).Select(x => x.TipoDeAmostra).Distinct().ToList())
            {
                var listaDaAmostra = listaDoLoteCef.Where(x => x.TipoDeAmostra == tipoDeAmostra).ToList();

                var detalhe = new DetalheProcessamentoQualidadeCefViewModel
                {
                    TipoDeAmostra = tipoDeAmostra,
                    TotalSelecionado = listaDaAmostra.Sum(x => x.QtdSelecionados),
                    TotalAprovado = listaDaAmostra.Sum(x => x.QtdAprovados),
                    TotalMarcado = listaDaAmostra.Sum(x => x.QtdMarcados),
                    TotalEmReprocessamento = listaDaAmostra.Sum(x => x.QtdEmReprocessamento)
                };

                detalhes.Add(detalhe);
            }

            return detalhes;
        }
    }
}
