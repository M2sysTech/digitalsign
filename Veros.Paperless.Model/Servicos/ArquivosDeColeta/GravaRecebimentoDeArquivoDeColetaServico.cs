namespace Veros.Paperless.Model.Servicos.ArquivosDeColeta
{
    using System;
    using System.IO;
    using Entidades;
    using Framework.Servicos;
    using Repositorios;

    public class GravaRecebimentoDeArquivoDeColetaServico : IGravaRecebimentoDeArquivoDeColetaServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IColetaRepositorio coletaRepositorio;
        private readonly IArquivoColetaRepositorio arquivoColetaRepositorio;

        public GravaRecebimentoDeArquivoDeColetaServico(ISessaoDoUsuario userSession,
            IArquivoColetaRepositorio arquivoColetaRepositorio,
            IColetaRepositorio coletaRepositorio)
        {
            this.userSession = userSession;
            this.arquivoColetaRepositorio = arquivoColetaRepositorio;
            this.coletaRepositorio = coletaRepositorio;
        }

        public void Executar(int coletaId, string caminhoArquivo)
        {
            var nomeArquivo = Path.GetFileName(caminhoArquivo);
            
            this.arquivoColetaRepositorio.FinalizarPendentes(coletaId);

            var arquivoColeta = new ArquivoColeta
            {
                Coleta = new Coleta { Id = coletaId },
                NomeArquivo = nomeArquivo,
                DataUpado = DateTime.Now,
                Status = ArquivoColeta.AguardandoAnalise,
                UsuarioUpado = (Usuario)this.userSession.UsuarioAtual,
                TamanhoBytes = (int)new System.IO.FileInfo(caminhoArquivo).Length
            };

            this.arquivoColetaRepositorio.Salvar(arquivoColeta);

            this.coletaRepositorio.AtualizarArquivo(coletaId, nomeArquivo);
        }
    }
}
