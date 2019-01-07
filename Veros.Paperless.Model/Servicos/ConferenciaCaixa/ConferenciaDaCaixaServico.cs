namespace Veros.Paperless.Model.Servicos.ConferenciaCaixa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Entidades;
    using Framework;
    using Framework.Servicos;
    using Repositorios;

    public class ConferenciaDaCaixaServico : IConferenciaDaCaixaServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;

        public ConferenciaDaCaixaServico(ISessaoDoUsuario userSession,
            IPacoteRepositorio pacoteRepositorio, 
            IDossieEsperadoRepositorio dossieEsperadoRepositorio)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
            this.userSession = userSession;
        }

        public void Executar(int caixaId, IList<string> dossies, IList<string> status)
        {
            var caixa = this.pacoteRepositorio.ObterComColetaPorId(caixaId);

            for (var i = 0; i < dossies.Count; i++)
            {
                this.SalvarDossie(caixa, dossies[i].ToInt(), status[i]);
            }

            caixa.Status = Pacote.FimConferencia;
            caixa.DataConferencia = DateTime.Now;
            caixa.UsuarioConferencia = (Usuario)this.userSession.UsuarioAtual;

            this.pacoteRepositorio.Salvar(caixa);
        }

        private void SalvarDossie(Pacote caixa, int dossieId, string dossieStatus)
        {
            var dossie = caixa.DossiesEsperados.FirstOrDefault(x => x.Id == dossieId);
            dossie.Status = dossieStatus;

            this.dossieEsperadoRepositorio.Salvar(dossie);
        }
    }
}
