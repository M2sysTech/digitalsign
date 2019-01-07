namespace Veros.Paperless.Model.Servicos.Dossies
{
    using Entidades;
    using Framework;
    using Framework.Modelo;
    using Repositorios;

    public class SalvaDossieEsperadoServico : ISalvaDossieEsperadoServico
    {
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;
        private readonly IProcessoRepositorio processoRepositorio;

        public SalvaDossieEsperadoServico(
            IDossieEsperadoRepositorio dossieEsperadoRepositorio, 
            IProcessoRepositorio processoRepositorio)
        {
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
            this.processoRepositorio = processoRepositorio;
        }

        public DossieEsperado Executar(DossieEsperado dossieEsperado)
        {
            if (this.Duplicado(dossieEsperado))
            {
                return null;
            }

            if (dossieEsperado.Id < 1)
            {
                dossieEsperado.Situacao = "DC";
                dossieEsperado.Status = "N";    
            }

            var dossieAtualizado = this.ObterAtualizado(dossieEsperado);
            
            var processo = dossieAtualizado.Processo();

            if (processo.NaoTemConteudo() == false)
            {
                this.processoRepositorio.AlterarIdentificacao(processo.Id, dossieAtualizado.IdentificacaoFormatada(), dossieAtualizado.CodigoDeBarra());
            }

            return dossieAtualizado;
        }

        private DossieEsperado ObterAtualizado(DossieEsperado dossieEsperado)
        {
            var dossieNoBanco = this.dossieEsperadoRepositorio.ObterComLotes(dossieEsperado.Id);

            if (dossieNoBanco.NaoTemConteudo())
            {
                this.dossieEsperadoRepositorio.Salvar(dossieEsperado);
                return dossieEsperado;
            }

            dossieNoBanco.NumeroContrato = dossieEsperado.NumeroContrato;
            dossieNoBanco.MatriculaAgente = dossieEsperado.MatriculaAgente;
            dossieNoBanco.CodigoDeBarras = dossieEsperado.CodigoDeBarras;
            dossieNoBanco.Hipoteca = dossieEsperado.Hipoteca;
            dossieNoBanco.NomeDoMutuario = dossieEsperado.NomeDoMutuario;
            dossieNoBanco.UfArquivo = dossieEsperado.UfArquivo;

            this.dossieEsperadoRepositorio.Salvar(dossieNoBanco);

            return dossieNoBanco;
        }
        
        private bool Duplicado(DossieEsperado dossieEsperado)
        {
            var dossieExistente = this.dossieEsperadoRepositorio.ObterDossie(dossieEsperado);

            if (dossieExistente != null && dossieExistente != dossieEsperado)
            {
                throw new RegraDeNegocioException("Dossiê já cadastrado");
            }

            return false;
        }
    }
}
