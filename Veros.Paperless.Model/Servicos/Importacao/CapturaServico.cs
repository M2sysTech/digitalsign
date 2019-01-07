namespace Veros.Paperless.Model.Servicos.Importacao
{
    using Entidades;
    using Repositorios;

    public class CapturaServico : ICapturaServico
    {
        private readonly IPacoteRepositorio pacoteRepositorio;
        private readonly ILoteRepositorio loteRepositorio;
        private readonly IPacoteFactory pacoteFactory;
        private readonly IProcessoRepositorio processoRepositorio;
        private readonly IPacoteProcessadoFactory pacoteProcessadoFactory;
        private readonly LogLoteServico logLoteServico;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public CapturaServico(
            IPacoteRepositorio pacoteRepositorio,
            ILoteRepositorio loteRepositorio,
            IPacoteFactory pacoteFactory,
            IProcessoRepositorio processoRepositorio,
            IPacoteProcessadoFactory pacoteProcessadoFactory,
            LogLoteServico logLoteServico,
            IUsuarioRepositorio usuarioRepositorio)
        {
            this.pacoteRepositorio = pacoteRepositorio;
            this.loteRepositorio = loteRepositorio;
            this.pacoteFactory = pacoteFactory;
            this.processoRepositorio = processoRepositorio;
            this.pacoteProcessadoFactory = pacoteProcessadoFactory;
            this.logLoteServico = logLoteServico;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public Lote Iniciar(Usuario usuario)
        {
            var pacoteDoDia = this.pacoteProcessadoFactory.ObterDoDia();

            var pacote = this.pacoteFactory.Criar();

            var lote = Lote.CriarNovo(pacote, LoteStatus.EmCaptura, pacoteDoDia);
            var processo = Processo.CriarNovo(lote);

            this.pacoteRepositorio.Salvar(pacote);
            this.loteRepositorio.Salvar(lote);
            this.processoRepositorio.Salvar(processo);

            return lote;
        }

        public Lote IniciarComCpf(string cpf, string token)
        {
            var pacoteDoDia = this.pacoteProcessadoFactory.ObterDoDia();

            var usuarioLogado = this.usuarioRepositorio.ObterPorToken(token);

            var pacote = this.pacoteFactory.Criar();

            var lote = Lote.CriarNovo(pacote, LoteStatus.EmCaptura, pacoteDoDia);
            
            lote.Identificacao = cpf;
            lote.UsuarioCaptura = usuarioLogado;

            var processo = Processo.CriarNovo(lote);

            this.pacoteRepositorio.Salvar(pacote);
            this.loteRepositorio.Salvar(lote);
            this.processoRepositorio.Salvar(processo);

            this.logLoteServico.Gravar(lote.Id, "LI", "recebido informação do Cpf :: {0}", token, cpf);

            return lote;
        }

        public void EncerrarCaptura(int loteId)
        {
            this.loteRepositorio.AlterarStatus(loteId, LoteStatus.EmRecepcao);
        }

        public Processo Iniciar(ImagemConta proposta)
        {
            var pacoteDoDia = this.pacoteProcessadoFactory.ObterDoDia();

            var pacote = this.pacoteFactory.Criar();

            var lote = Lote.CriarNovo(pacote, LoteStatus.SetaClassifier, pacoteDoDia);
            lote.Identificacao = proposta.Cpf;

            var processo = Processo.CriarNovo(lote);

            this.pacoteRepositorio.Salvar(pacote);
            this.loteRepositorio.Salvar(lote);
            this.processoRepositorio.Salvar(processo);

            return processo;
        } 
    }
}