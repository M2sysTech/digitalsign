namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Entidades;
    using Repositorios;

    public class FinalizaImportadosServico : IFinalizaImportadosServico
    {
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;

        public FinalizaImportadosServico(IPacoteRepositorio pacoteRepositorio, 
            IDossieEsperadoRepositorio dossieEsperadoRepositorio)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
        }

        public void Confirmar(Coleta coleta)
        {
            this.pacoteRepositorio.AlterarStatusPorColeta(coleta, Pacote.Recebido);
        }

        public void Cancelar(Coleta coleta)
        {
            this.dossieEsperadoRepositorio.ExcluirDossiesPorColeta(coleta.Id);
            this.pacoteRepositorio.ApagarPorColeta(coleta, Pacote.Importando);
        }
    }
}
