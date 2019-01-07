namespace Veros.Paperless.Model.Servicos.Reconhecimento
{
    using Entidades;
    using Image.Reconhecimento;
    using Repositorios;

    public class SalvarReconhecimentoPaginaServico : ISalvarReconhecimentoPaginaServico
    {
        private readonly IPaginaRepositorio paginaRepositorio;
        private readonly IValorReconhecidoRepositorio valorReconhecidoRepositorio;
        private readonly IObtemValoresReconhecidosPaginaServico obtemValoresReconhecidosPaginaServico;
        private readonly IPalavraReconhecidaRepositorio palavraRepositorio;

        public SalvarReconhecimentoPaginaServico(
            IPaginaRepositorio paginaRepositorio,
            IValorReconhecidoRepositorio valorReconhecidoRepositorio,
            IObtemValoresReconhecidosPaginaServico obtemValoresReconhecidosPaginaServico,
            IPalavraReconhecidaRepositorio palavraRepositorio)
        {
            this.paginaRepositorio = paginaRepositorio;
            this.valorReconhecidoRepositorio = valorReconhecidoRepositorio;
            this.obtemValoresReconhecidosPaginaServico = obtemValoresReconhecidosPaginaServico;
            this.palavraRepositorio = palavraRepositorio;
        }

        public void Executar(Pagina pagina, ResultadoReconhecimento resultadoReconhecimento)
        {
            if (resultadoReconhecimento == null)
            {
                return;
            }

            var valoresReconhecidos = this.obtemValoresReconhecidosPaginaServico.Obter(
                pagina,
                resultadoReconhecimento);

            foreach (var valorReconhecido in valoresReconhecidos)
            {
                this.valorReconhecidoRepositorio.Salvar(valorReconhecido);
            }

            var tripaTexto = string.Empty;
            foreach (var palavra in resultadoReconhecimento.Palavras)
            {
                var palavraReconhecida = new PalavraReconhecida
                {
                    Texto = palavra.Texto,
                    Altura = palavra.Localizacao.Height,
                    Direita = palavra.Localizacao.Right,
                    Esquerda = palavra.Localizacao.Left,
                    Largura = palavra.Localizacao.Width,
                    Topo = palavra.Localizacao.Top,
                    Pagina = pagina
                };

                this.palavraRepositorio.Salvar(palavraReconhecida);
                tripaTexto += palavra.Texto + " ";
            }

            //// gravando parte das palavras reconhecidas na recvalue também, para facilitar comparações na POC. 
            if (tripaTexto.Trim().Length > 2)
            {
                if (tripaTexto.Length > 200)
                {
                    tripaTexto = tripaTexto.Substring(0, 199) + "(...)";
                }

                var recValueFull = new ValorReconhecido() { TemplateName = "fulltextbsb", CampoTemplate = "fulltextbsb", Pagina = pagina, Value = tripaTexto };
                this.valorReconhecidoRepositorio.Salvar(recValueFull);
            }
        }
    }
}
