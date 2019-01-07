namespace Veros.Paperless.Model.Servicos.SeparacaoServiceEngine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entidades;
    using Framework;
    using Repositorios;
    using Veros.Paperless.Model.Servicos.Batimento;

    public class IdentificaTipoDocumentoServico : IIdentificaTipoDocumentoServico
    {
        public ITesseractServico TesseractServico;
        private readonly IPalavraTipoRepositorio palavraTipoRepositorio;

        public IdentificaTipoDocumentoServico(ITesseractServico tesseractServico, 
            IPalavraTipoRepositorio palavraTipoRepositorio)
        {
            this.TesseractServico = tesseractServico;
            this.palavraTipoRepositorio = palavraTipoRepositorio;
            this.TesseractServico.SetarDiretorioTessdata(Contexto.DiretorioTessdata);
        }

        public void Executar(List<List<Pagina>> todasAsPaginas, string cacheLocalImagens)
        {
            foreach (var itemDocumental in todasAsPaginas)
            {
                Pagina primeiraPagina = itemDocumental.FirstOrDefault(x => x.Status != PaginaStatus.StatusExcluida && x.Separadora == false && x.ContrapartidaDeSeparadora == false && x.EmBranco == false);

                if (primeiraPagina == null)
                {
                    Log.Application.InfoFormat("Nenhuma pagina válida no item documental atual");
                    return;
                }

                var caminhoArquivoBaixado = Path.Combine(cacheLocalImagens, primeiraPagina.Id.ToString("000000000") + "." + primeiraPagina.TipoArquivo.ToUpper());
                if (primeiraPagina.OrientacaoTesseract > 0)
                {
                    if (File.Exists(caminhoArquivoBaixado.Replace(".JPG", "_Girada.JPG")))
                    {
                        caminhoArquivoBaixado = caminhoArquivoBaixado.Replace(".JPG", "_Girada.JPG");    
                    }
                }

                var palavrasRaw = this.TesseractServico.ReconhecerPalavrasTopo(caminhoArquivoBaixado, Contexto.PercentRegiaoTopoOcr).ToUpper();
                primeiraPagina.PalavrasReconhecidasOcr = this.LimparTextoBasico(palavrasRaw);
                var textParaDebug = primeiraPagina.PalavrasReconhecidasOcr;
                if (textParaDebug.Length > 200)
                {
                    textParaDebug = textParaDebug.Substring(0, 200) + "(...)";
                }

                Log.Application.DebugFormat("Caracteres extraidos via OCR ({0}). texto: {1}", textParaDebug.Length, textParaDebug);
                var listaPalavras = this.palavraTipoRepositorio.ObterTodos().ToList();
                primeiraPagina.TipoDocumentoDefinidoPorOcr = this.CruzarPalarasComReconhecimento(primeiraPagina, listaPalavras);
                if (primeiraPagina.TipoDocumentoDefinidoPorOcr.Id == TipoDocumento.CodigoNaoIdentificado)
                {
                    Log.Application.DebugFormat("Tipo Documento não foi reconhecido. Ocr analisado na pagina #{0}", primeiraPagina.Id);
                }
                else
                {
                    Log.Application.DebugFormat("Tipo Documento reconhecido como [{0}]. Ocr analisado na pagina #{1}", primeiraPagina.TipoDocumentoDefinidoPorOcr, primeiraPagina.Id);
                }
            }
        }

        public TipoDocumento CruzarPalarasComReconhecimento(Pagina pagina, List<PalavraTipo> listaPalavras)
        {
            if (string.IsNullOrEmpty(pagina.PalavrasReconhecidasOcr))
            {
                return new TipoDocumento() { Id = TipoDocumento.CodigoNaoIdentificado };
            }

            var tiposComPalavrasMapeadas = listaPalavras.GroupBy(x => x.TipoDocumento).Select(y => y.First().TipoDocumento).OrderBy(z => z.QuantidadeDePaginas);

            foreach (var tipoAtual in tiposComPalavrasMapeadas)
            {
                if (this.TestarPalavrasParaPagina(listaPalavras.Where(x => x.TipoDocumento.Id == tipoAtual.Id).ToList(), pagina))
                {
                    return tipoAtual;
                }
            }

            return new TipoDocumento() { Id = TipoDocumento.CodigoNaoIdentificado };
        }

        public string LimparTextoBasico(string palavrasRaw)
        {
            if (string.IsNullOrEmpty(palavrasRaw))
            {
                return string.Empty;
            }

            palavrasRaw = palavrasRaw.ToUpper().RemoverDiacritico();
            palavrasRaw = palavrasRaw.Replace("\n", " ");
            palavrasRaw = palavrasRaw.Replace("  ", " ");
            return palavrasRaw;
        }

        private bool TestarPalavrasParaPagina(List<PalavraTipo> listaPalavras, Pagina pagina)
        {
            if (listaPalavras == null || string.IsNullOrEmpty(pagina.PalavrasReconhecidasOcr))
            {
                return false;
            }

            if (listaPalavras.Count == 0)
            {
                return false;
            }

            //// testando palavras requeridas
            foreach (var palavraTipo in listaPalavras.Where(x => x.Categoria == PalavraTipoCategoria.Requerida))
            {
                if (pagina.PalavrasReconhecidasOcr.IndexOf(palavraTipo.Texto.ToUpper(), StringComparison.InvariantCulture) < 0)
                {
                    pagina.PalavrasTipos.Add(palavraTipo);
                    return false;
                }
            }

            //// testando palavras proibidas
            foreach (var palavraTipo in listaPalavras.Where(x => x.Categoria == PalavraTipoCategoria.Proibida))
            {
                if (pagina.PalavrasReconhecidasOcr.IndexOf(palavraTipo.Texto.ToUpper(), StringComparison.InvariantCulture) >= 0)
                {
                    pagina.PalavrasTipos.Add(palavraTipo);
                    return false;
                }
            }

            //// testando qualquer uma das palavras opcionais
            foreach (var palavraTipo in listaPalavras.Where(x => x.Categoria == PalavraTipoCategoria.Opcional))
            {
                if (pagina.PalavrasReconhecidasOcr.IndexOf(palavraTipo.Texto.ToUpper(), StringComparison.InvariantCulture) >= 0)
                {
                    pagina.PalavrasTipos.Add(palavraTipo);
                    return true;
                }
            }

            var palavraTipoFalhou = new PalavraTipo()
            {
                Categoria = PalavraTipoCategoria.Falha,
                TipoDocumento = listaPalavras.First().TipoDocumento,
                Id = 0,
                Texto = "falhou batimento"
            };
            pagina.PalavrasTipos.Add(palavraTipoFalhou);
            return false;
        }
    }
}