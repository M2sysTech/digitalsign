namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Collections.Generic;
    using Veros.Paperless.Model.Entidades;
    using System;
    using System.IO;
    using Veros.Framework;

    public class ExtraiDadosDoArquivoOcr : IExtraiDadosDoArquivoOcr
    {
        private readonly ValorReconhecidoFabrica valorReconhecidoFabrica;

        public ExtraiDadosDoArquivoOcr(ValorReconhecidoFabrica valorReconhecidoFabrica)
        {
            this.valorReconhecidoFabrica = valorReconhecidoFabrica;
        }

        public List<ValorReconhecido> Execute(Processo processo, string nomeDaPasta)
        {
            var lista = processo.Documentos;
            var retornoValores = new List<ValorReconhecido>();
            foreach (var documento in lista)
            {
                var paginasDoDocAtual = documento.Paginas;
                foreach (var pagina in paginasDoDocAtual)
                {
                    ////procura se existe arquivo OCR para a pagina atual
                    var arquivoAtual = this.ExisteArquivoOcr(pagina, nomeDaPasta);
                    if (arquivoAtual != string.Empty)
                    {
                        //// monta lista de campos reconhecidos a partir do texto
                        var campos = this.ExtrairCamposDoArquivoAtual(arquivoAtual);
                        //// cria objeto valorReconhecido (tabela recValue)
                        var valoresLidos = this.valorReconhecidoFabrica.Criar(pagina, campos);
                        retornoValores.AddRange(valoresLidos);
                    }
                }
            }

            return retornoValores;
        }

        public string ExisteArquivoOcr(Pagina pagina, string nomeDaPasta)
        {
            var nomeCompleto = Path.Combine(nomeDaPasta, string.Format("{0}.{1}", pagina.NomeArquivoSemExtensao, "ocr"));
            if (File.Exists(nomeCompleto))
            {
                return nomeCompleto;
            }

            return string.Empty;
        }

        public List<Tuple<string, string>> ExtrairCamposDoArquivoAtual(string nomeArquivo)
        {
            int counter = 0;
            string linha = null;
            var resultado = new List<Tuple<string, string>>();
            System.IO.StreamReader file;

            try
            {
                file = new System.IO.StreamReader(nomeArquivo);
                linha = file.ReadLine();
            }
            catch (Exception e)
            {
                Log.Application.DebugFormat("Erro ao abrir arquivo OCR [{0}]", nomeArquivo);
                return resultado;
            }
            
            while (linha != null)
            {
                if (linha != string.Empty && linha.Substring(0, 1) == "¨")
                {
                    var label = linha.Right(linha.Length - 1).Replace(":", string.Empty);
                    //// Somente para POC: não vamos tratar fulltext nesse momento. 
                    if (label.ToLower() == "fulltext")
                    {
                        linha = null;
                        break;
                    }

                    linha = file.ReadLine();
                    var conteudo = string.Empty;
                    while (!string.IsNullOrEmpty(linha) && linha.Substring(0, 1) != "¨") 
                    {
                        conteudo += linha + " ";
                        linha = file.ReadLine();
                    }

                    var tuplaAtual = new Tuple<string, string>(label, conteudo.Trim());
                    resultado.Add(tuplaAtual);
                }
                else
                {
                    linha = file.ReadLine();
                }
            }

            file.Close();

            return resultado;
        }
    }
}
