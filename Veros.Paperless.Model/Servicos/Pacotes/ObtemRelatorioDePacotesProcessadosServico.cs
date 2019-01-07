namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using System;
    using System.Collections.Generic;
    using Consultas;
    using Entidades;
    using Repositorios;
    using System.Linq;

    public class ObtemRelatorioDePacotesProcessadosServico : IObtemRelatorioDePacotesProcessadosServico
    {
        private readonly IPacoteProcessadoRepositorio estatisticaPacotesProcessadosRepositorio;

        public ObtemRelatorioDePacotesProcessadosServico(IPacoteProcessadoRepositorio estatisticaPacotesProcessadosRepositorio)
        {
            this.estatisticaPacotesProcessadosRepositorio = estatisticaPacotesProcessadosRepositorio;
        }

         public IList<PacotesProcessadosConsulta> Obter(DateTime dataInicio, DateTime dataFim)
         {
             var pacotesProcessados = new List<PacotesProcessadosConsulta>();
             var listaPacotesProcessados = this.estatisticaPacotesProcessadosRepositorio.ObterPorPeriodo(dataInicio, dataFim);
             
             for (DateTime data = dataInicio; data <= dataFim; data = data.AddDays(1))
             {
                 var pacotes = new PacotesProcessadosConsulta()
                 {
                    DataRegistro = data,
                    TotalDePacotesCancelados = this.SomaDeTodosPacotesCancelados(data, listaPacotesProcessados),
                    TotalDePacotesExportados = this.SomaDeTodosPacotesExportados(data, listaPacotesProcessados),
                    TotalDePacotesImportados = this.SomaDeTodosPacotesImportados(data, listaPacotesProcessados)
                 };

                pacotesProcessados.Add(pacotes);
             }

             return pacotesProcessados;
         }

        private int SomaDeTodosPacotesImportados(DateTime data, IList<PacoteProcessado> listaPacotesProcessados)
        {
            return listaPacotesProcessados.Count(x => x.ArquivoRecebidoEm.Value.Date.Equals(data.Date));   
        }

        private int SomaDeTodosPacotesExportados(DateTime data, IList<PacoteProcessado> listaPacotesProcessados)
        {
            return listaPacotesProcessados.Count(x => x.StatusPacote.Equals(StatusPacote.Processado) 
                && x.ArquivoRecebidoEm.Value.Date.Equals(data.Date));   
        }

        private int SomaDeTodosPacotesCancelados(DateTime data, IList<PacoteProcessado> listaPacotesProcessados)
        {
            return listaPacotesProcessados.Count(x => x.StatusPacote.Equals(StatusPacote.Cancelado) 
                && x.ArquivoRecebidoEm.Value.Date.Equals(data));
        }
    }
}
