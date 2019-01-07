namespace Veros.Paperless.Model.Servicos.RecepcaoColeta
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Entidades;
    using Framework;
    using Framework.Servicos;
    using Repositorios;

    public class RecepcaoDeColetaServico : IRecepcaoDeColetaServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IColetaRepositorio coletaRepositorio;
        private readonly IPacoteRepositorio pacoteRepositorio;

        public RecepcaoDeColetaServico(ISessaoDoUsuario userSession, 
            IColetaRepositorio coletaRepositorio, 
            IPacoteRepositorio pacoteRepositorio)
        {
            this.coletaRepositorio = coletaRepositorio;
            this.pacoteRepositorio = pacoteRepositorio;
            this.userSession = userSession;
        }

        public void Executar(int coletaId, IList<string> caixas, IList<string> status)
        {
            var coleta = this.coletaRepositorio.ObterComPacotesPorId(coletaId);

            for (var i = 0; i < caixas.Count; i++)
            {
                this.SalvarCaixa(coleta, caixas[i].ToInt(), status[i]);
            }

            coleta.Status = ColetaStatus.Recebido;
            coleta.DataRecepcao = DateTime.Now;
            coleta.UsuarioRecepcao = (Usuario) this.userSession.UsuarioAtual;
            coleta.QuantidadeDeCaixasTipo2 = status.Count(x => x == "ok");

            this.coletaRepositorio.Salvar(coleta);
        }

        private void SalvarCaixa(Coleta coleta, int caixaId, string caixaStatus)
        {
            var pacote = coleta.Pacotes.FirstOrDefault(x => x.Id == caixaId);
            pacote.Status = Pacote.Recebido;

            if (caixaStatus == "P")
            {
                pacote.Status = Pacote.NaoRecebidoNaRecepcao;
            }

            this.pacoteRepositorio.Salvar(pacote);
        }
    }
}
