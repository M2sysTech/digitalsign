namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using Comparacao;
    using Entidades;
    using Framework;

    public class BatimentoFullText
    {
        public static string ImprimirLista(List<dynamic> listaLimpa)
        {
            if (listaLimpa == null)
            {
                return string.Empty;
            }

            string auxiliar = string.Empty;
            foreach (var dynamic in listaLimpa)
            {
                auxiliar += dynamic.Texto + ",";
            }

            return auxiliar;
        }   

        public static string ImprimirLista(IList<PalavraReconhecida> listaLimpa)
        {
            if (listaLimpa == null)
            {
                return string.Empty;
            }

            string auxiliar = string.Empty;
            foreach (var dynamic in listaLimpa)
            {
                auxiliar += dynamic.Texto + ",";
            }

            return auxiliar;
        }

        public static string LimitaString(string texto, int tamanhoMax)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return string.Empty;
            }

            if (texto.Length <= tamanhoMax)
            {
                return texto;
            }

            return texto.Substring(0, tamanhoMax - 4) + "...";
        }

        public bool ComparaFiliacaoCnhNomeAbreviado(Indexacao indexacao, string nomeDeFiliacao)
        {
            try
            {
                if (string.IsNullOrEmpty(nomeDeFiliacao) || indexacao == null)
                {
                    return false;
                }

                var listaNomesPrimeiroValor = Equivalencia.LimpaConteudoNome(indexacao.SegundoValor).Split(' ').ToList();
                var listaNomesAchadosNoOcr = Equivalencia.LimpaConteudoNome(nomeDeFiliacao.Replace("0", "O")).Split(' ').ToList();

                if (listaNomesPrimeiroValor.Count < 2 || listaNomesAchadosNoOcr.Count < 2)
                {
                    return false;
                }

                ////primeiro nome não pode ser abreviado e precisa ser igual 
                if (!listaNomesPrimeiroValor[0].Equals(listaNomesAchadosNoOcr[0]))
                {
                    return false;
                }

                //// compara o miolo do nome considerando abreviação
                var j = 1;
            
                for (int i = 1; i < listaNomesPrimeiroValor.Count; i++)
                {
                    if (listaNomesPrimeiroValor[i].Length == 1) 
                    {
                        if (j >= listaNomesAchadosNoOcr.Count)
                        {
                            return false;
                        }

                        //// esta abreviado...
                        if (!listaNomesPrimeiroValor[i][0].Equals(listaNomesAchadosNoOcr[j][0]))
                        {
                            return false;
                        }

                        //// verifica se a palavra abreviada é uma preposicao 
                        var listaPreposicoes = new List<string> { "DE", "DO", "DOS", "DA", "DAS" };
                        if (listaPreposicoes.Contains(listaNomesAchadosNoOcr[j].ToUpper()))
                        {
                            return false;
                        }

                        j++;
                    }
                    else
                    {
                        //// nome sem abreviação
                        if (!listaNomesPrimeiroValor[i].Equals(listaNomesAchadosNoOcr[j]))
                        {
                            ////verifica se houve quebra de linha (concatena com a proxima palavra)
                            if (j >= listaNomesAchadosNoOcr.Count - 1)
                            {
                                return false;
                            }

                            var palavaConcatenada = listaNomesAchadosNoOcr[j] + listaNomesAchadosNoOcr[j + 1];
                            if (!listaNomesPrimeiroValor[i].Equals(palavaConcatenada))
                            {
                                //// ultima chance... se a palavra anterior estava abreviada, o conteudo atual de ocr (j) pode ser sobra de quebra de linha dessa abreviaçao 
                                //// Exemplo: Cadastro=MARIA O MARTINS      Ocr=MARIA OLIV EIRA MARTINS
                                if (listaNomesPrimeiroValor[i - 1].Length == 1 && listaNomesAchadosNoOcr.Count == listaNomesPrimeiroValor.Count + 1)
                                {
                                    i--;
                                    j++;
                                    continue;
                                }

                                return false;
                            }

                            //// avança duas posições em j (uma delas aqui, a outra depois do if)
                            j++;
                        }

                        j++;
                    }
                }

                //// verifica se sobrou alguma palavra reconhecida a mais  (caso do ultimo sobrenome faltando no cadastro)
                if (listaNomesAchadosNoOcr.Count > j)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        public bool ComparaFiliacaoCnhPrimeiroEUltimo(Indexacao indexacao, string nomeDeFiliacao)
        {
            if (string.IsNullOrEmpty(nomeDeFiliacao) || indexacao == null)
            {
                return false;
            }

            var listaNomesPrimeiroValor = Equivalencia.LimpaConteudoNome(indexacao.SegundoValor).Split(' ').ToList();
            var listaNomesAchadosNoOcr = Equivalencia.LimpaConteudoNome(nomeDeFiliacao.Replace("0", "O")).Split(' ').ToList();

            if (listaNomesPrimeiroValor.Count < 2 || listaNomesAchadosNoOcr.Count < 2)
            {
                return false;
            }

            if (listaNomesPrimeiroValor[0].Equals(listaNomesAchadosNoOcr[0]))
            {
                if (listaNomesPrimeiroValor[listaNomesPrimeiroValor.Count - 1].Equals(listaNomesAchadosNoOcr[listaNomesAchadosNoOcr.Count - 1]))
                {
                    return true;
                }

                if (listaNomesAchadosNoOcr.Count > 2)
                {
                    var nomeDeQuebraDeLinha = listaNomesAchadosNoOcr[listaNomesAchadosNoOcr.Count - 2] + listaNomesAchadosNoOcr[listaNomesAchadosNoOcr.Count - 1];
                    if (listaNomesPrimeiroValor[listaNomesPrimeiroValor.Count - 1].Equals(nomeDeQuebraDeLinha))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<dynamic> MontarListaFiliacaoCnh(IList<PalavraReconhecida> palavras)
        {
            var resultado = new List<dynamic>();
            if (palavras == null)
            {
                return resultado;
            }

            if (palavras.Count == 0)
            {
                return resultado;
            }

            var listadatas = new OcorrenciaCampoDataNaCnh();
            listadatas.LocalizarDatas(palavras);

            if (listadatas.QuantidadeDatasEncontradas < 2)
            {
                return resultado;
            }

            if (listadatas.PossuiDistanciaMinimaEntreDatas()) 
            {
                return resultado;
            }

            //// monta área de busca (retangulo) para delimitar palavras encontradas
            //// o retângulo de busca corresponde exatamente ao quadrado "Filiação" da CNH, usando as posições de data encontradas anteriormente como âncoras
            var retanguloFiliacao = new AreaRetangularDeBusca(listadatas);

            //// devole apenas as palavras dentro do retângulo de busca
            for (int i = listadatas.PosicaoPrimeiraDataNaListaOriginal; i < listadatas.PosicaoUltimaDataNaListaOriginal; i++)
            {
                var dynamic = palavras[i];
                if (this.RetContemRet(retanguloFiliacao.Retangulo, dynamic.Localizacao))
                {
                    ////resultado.Add(new dynamic()
                    ////{
                    ////    Localizacao = dynamic.Localizacao,
                    ////    Texto = dynamic.Texto.RemoveAcentuacao()
                    ////            .RemoverDiacritico()
                    ////            .RemoveCaracteresNaoAlfa(true)
                    ////            .ToLower()
                    ////            .Trim()
                    ////});
                }
            }

            return resultado;
        }

        public string ExtrairNomeMaePrimeiroeUltimo(IList<dynamic> listagemDeFiliacao)
        {
            if (listagemDeFiliacao == null)
            {
                return string.Empty;
            }

            if (listagemDeFiliacao.Count == 0)
            {
                return string.Empty;
            }

            var nomeMae = string.Empty;
            var arrayPalavrasLinhas = this.AnalisePalavrasPorLinha(listagemDeFiliacao);
            if (arrayPalavrasLinhas.Count == 1)
            {
                nomeMae = listagemDeFiliacao[0].Texto + " ";
                if (listagemDeFiliacao.Count > 1)
                {
                    nomeMae = nomeMae + listagemDeFiliacao[listagemDeFiliacao.Count - 1].Texto + " ";
                }
            }

            if (arrayPalavrasLinhas.Count == 2)
            {
                nomeMae = listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0)].Texto + " ";
                nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1) - 1].Texto + " ";
            }

            if (arrayPalavrasLinhas.Count == 3)
            {
                if (arrayPalavrasLinhas[1] < arrayPalavrasLinhas[2] && arrayPalavrasLinhas[1] < arrayPalavrasLinhas[0])
                {
                    ////linhas 0 e 1 são nome do pai
                    nomeMae = listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1)].Texto + " ";
                    nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1, 2) - 1].Texto + " ";
                }
                else
                {
                    //// apenas linha 0 é nome do pai (1 e 2 são da mãe)
                    nomeMae = listagemDeFiliacao[arrayPalavrasLinhas[0]].Texto + " ";
                    if (arrayPalavrasLinhas[2] > 1)
                    {
                        //// mais de uma palavra na ultima linha, pegar só a última
                        nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1, 2) - 1].Texto + " ";
                    }
                    else
                    {
                        //// somente uma palavra na ultima linha, retorna primeiro nome + ultima palavra linha 1 + unica palavra linha 2
                        nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1) - 1].Texto + " ";
                        nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1, 2) - 1].Texto + " ";
                    }
                }
            }

            if (arrayPalavrasLinhas.Count == 4)
            {
                ////linhas 0 e 1 são nome do pai, linhas 2 e 3 são da mãe
                nomeMae = listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1)].Texto + " ";
                if (arrayPalavrasLinhas[3] > 1)
                {
                    //// mais de uma palavra na ultima linha, pegar só a última
                    nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1, 2, 3) - 1].Texto + " ";
                }
                else
                {
                    //// somente uma palavra na ultima linha, retorna primeiro nome + ultima palavra linha 2 + unica palavra linha 3
                    nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1, 2) - 1].Texto + " ";
                    nomeMae = nomeMae + listagemDeFiliacao[this.SomarArray(arrayPalavrasLinhas, 0, 1, 2, 3) - 1].Texto + " ";
                }
            }

            nomeMae = nomeMae.Left(nomeMae.Length - 1);
            return nomeMae;
        }

        public string ExtrairNomeMaeCompleto(IList<dynamic> listagemDeFiliacao)
        {
            try
            {
                if (listagemDeFiliacao == null)
                {
                    return string.Empty;
                }

                if (listagemDeFiliacao.Count == 0)
                {
                    return string.Empty;
                }

                var nomeMae = string.Empty;
                var arrayPalavrasLinhas = this.AnalisePalavrasPorLinha(listagemDeFiliacao);
                if (arrayPalavrasLinhas.Count == 1)
                {
                    foreach (var dynamic in listagemDeFiliacao)
                    {
                        nomeMae += dynamic.Texto + " ";
                    }
                }

                if (arrayPalavrasLinhas.Count == 2)
                {
                    for (int i = this.SomarArray(arrayPalavrasLinhas, 0); i < listagemDeFiliacao.Count; i++)
                    {
                        nomeMae += listagemDeFiliacao[i].Texto + " ";
                    }
                }

                if (arrayPalavrasLinhas.Count == 3)
                {
                    if (arrayPalavrasLinhas[1] < arrayPalavrasLinhas[2] && arrayPalavrasLinhas[1] < arrayPalavrasLinhas[0])
                    {
                        ////linhas 0 e 1 são nome do pai
                        for (int i = this.SomarArray(arrayPalavrasLinhas, 0, 1); i < listagemDeFiliacao.Count; i++)
                        {
                            nomeMae += listagemDeFiliacao[i].Texto + " ";
                        }
                    }
                    else
                    {
                        //// apenas linha 0 é nome do pai (1 e 2 são da mãe)
                        for (int i = this.SomarArray(arrayPalavrasLinhas, 0); i < listagemDeFiliacao.Count; i++)
                        {
                            nomeMae += listagemDeFiliacao[i].Texto + " ";
                        }
                    }
                }

                if (arrayPalavrasLinhas.Count == 4)
                {
                    ////linhas 0 e 1 são nome do pai, linhas 2 e 3 são da mãe
                    for (int i = this.SomarArray(arrayPalavrasLinhas, 0, 1); i < listagemDeFiliacao.Count; i++)
                    {
                        nomeMae += listagemDeFiliacao[i].Texto + " ";
                    }
                }

                nomeMae = nomeMae.Left(nomeMae.Length - 1);
                return nomeMae;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string ExtrairNomepaiPrimeiroeUltimo(IList<dynamic> listagemDeFiliacao)
        {
            if (listagemDeFiliacao == null)
            {
                return string.Empty;
            }

            if (listagemDeFiliacao.Count == 0)
            {
                return string.Empty;
            }

            var nomePai = string.Empty;
            var arrayPalavrasLinhas = this.AnalisePalavrasPorLinha(listagemDeFiliacao);
            if (arrayPalavrasLinhas.Count == 1)
            {
                nomePai = listagemDeFiliacao[0].Texto + " ";
                if (listagemDeFiliacao.Count > 1)
                {
                    nomePai = nomePai + listagemDeFiliacao[listagemDeFiliacao.Count - 1].Texto + " ";
                }
            }

            if (arrayPalavrasLinhas.Count == 2)
            {
                nomePai = listagemDeFiliacao[0].Texto + " ";
                var regiaoAnterior = listagemDeFiliacao[0];
                for (int i = 1; i < listagemDeFiliacao.Count; i++)
                {
                    var regiaoAtual = listagemDeFiliacao[i];
                    if (this.VerificaMesmaLinha(regiaoAnterior, regiaoAtual))
                    {
                        regiaoAnterior = listagemDeFiliacao[i];
                    }
                    else
                    {
                        nomePai = nomePai + regiaoAnterior.Texto + " ";
                        break;
                    }
                }
            }

            if (arrayPalavrasLinhas.Count == 3 || arrayPalavrasLinhas.Count == 4)
            {
                nomePai = listagemDeFiliacao[0].Texto + " ";
                if (arrayPalavrasLinhas[1] < arrayPalavrasLinhas[2] && arrayPalavrasLinhas[1] < arrayPalavrasLinhas[0])
                {
                    ////linhas 0 e 1 são nome do pai
                    if (arrayPalavrasLinhas[1] > 1)
                    {
                        //// mais de uma palavra na segunda linha, pegar só a última
                        var contadorePalavras = arrayPalavrasLinhas[0] + arrayPalavrasLinhas[1] - 1;
                        nomePai = nomePai + listagemDeFiliacao[contadorePalavras].Texto + " ";
                    }
                    else
                    {
                        //// somente uma palavra na segunda linha, retorna primeiro nome + ultima palavra linha 0 + unica palavra linha 1
                        nomePai = nomePai + listagemDeFiliacao[arrayPalavrasLinhas[0] - 1].Texto + " ";
                        nomePai = nomePai + listagemDeFiliacao[arrayPalavrasLinhas[0] + arrayPalavrasLinhas[1] - 1].Texto + " ";
                    }
                }
                else
                {
                    //// apenas linha 0 é nome do pai
                    nomePai = nomePai + listagemDeFiliacao[arrayPalavrasLinhas[0] - 1].Texto + " ";
                }
            }

            nomePai = nomePai.Left(nomePai.Length - 1);
            return nomePai;
        }

        public string ExtrairNomePaiCompleto(IList<dynamic> listagemDeFiliacao)
        {
            if (listagemDeFiliacao == null)
            {
                return string.Empty;
            }

            if (listagemDeFiliacao.Count == 0)
            {
                return string.Empty;
            }

            var nomePai = string.Empty;
            var arrayPalavrasLinhas = this.AnalisePalavrasPorLinha(listagemDeFiliacao);
            if (arrayPalavrasLinhas.Count == 1)
            {
                foreach (var dynamic in listagemDeFiliacao)
                {
                    nomePai += dynamic.Texto + " ";
                }
            }

            if (arrayPalavrasLinhas.Count == 2)
            {
                ////nome pai está somente na linha 0 (linha 1 deve ser a mãe) 
                for (int i = 0; i < this.SomarArray(arrayPalavrasLinhas, 0); i++)
                {
                    nomePai += listagemDeFiliacao[i].Texto + " ";
                }
            }

            if (arrayPalavrasLinhas.Count == 3 || arrayPalavrasLinhas.Count == 4)
            {
                if (arrayPalavrasLinhas[1] < arrayPalavrasLinhas[2] && arrayPalavrasLinhas[1] < arrayPalavrasLinhas[0])
                {
                    ////linhas 0 e 1 são nome do pai
                    for (int i = 0; i < this.SomarArray(arrayPalavrasLinhas, 0, 1); i++)
                    {
                        nomePai += listagemDeFiliacao[i].Texto + " ";
                    }
                }
                else
                {
                    //// apenas linha 0 é nome do pai
                    for (int i = 0; i < this.SomarArray(arrayPalavrasLinhas, 0); i++)
                    {
                        nomePai += listagemDeFiliacao[i].Texto + " ";
                    }
                }
            }

            nomePai = nomePai.Left(nomePai.Length - 1);
            return nomePai;
        }

        public List<dynamic> ExtrairTextoGenerico(IList<PalavraReconhecida> palavras)
        {
            var listaLimpa = new List<dynamic>();
            foreach (var regiao in palavras)
            {
                var palavraAtual = Equivalencia.LimpaConteudoNome(regiao.Texto);
                if (string.IsNullOrEmpty(palavraAtual) == false)
                {
                    ////listaLimpa.Add(new dynamic()
                    ////{
                    ////    Texto = palavraAtual,
                    ////    Localizacao = regiao.Localizacao
                    ////});
                }
            }

            return listaLimpa;
        }

        public bool Batido(string palavra, IList<PalavraReconhecida> palavras)
        {
            var palavrasParaAnaliseFullTextGenerica = this.ExtrairTextoGenerico(palavras);

            var nomeCompleto = palavra
                .RemoverDiacritico()
                .ToLower()
                .Trim();

            return palavrasParaAnaliseFullTextGenerica
                .Any(x => x.Texto.RemoverDiacritico() == nomeCompleto);
        }

        private int SomarArray(List<int> listaInt, params int[] parametros)
        {
            var resultado = 0;
            foreach (var parametro in parametros)
            {
                resultado = resultado + listaInt[parametro];
            }

            return resultado;
        }

        private List<int> AnalisePalavrasPorLinha(dynamic listagemDeFiliacao)
        {
            var resultado = new List<int>();
            if (listagemDeFiliacao == null)
            {
                return resultado;
            }

            var qtdePalavras = 0;
            dynamic regiaoAnterior;

            foreach (var x in listagemDeFiliacao)
            {
                ////if (regiaoAnterior.Texto == null)
                ////{
                ////    qtdePalavras = 1;
                ////    regiaoAnterior = x;
                ////    continue;
                ////}

                ////if (this.VerificaMesmaLinha(x, regiaoAnterior))
                ////{
                ////    qtdePalavras++;
                ////}
                ////else
                ////{
                ////    resultado.Add(qtdePalavras);
                ////    qtdePalavras = 1;
                ////}

                regiaoAnterior = x;
            }

            resultado.Add(qtdePalavras);
            return resultado;
        }

        private bool VerificaMesmaLinha(dynamic a, dynamic regiaoAnterior)
        {
            if (regiaoAnterior == null || a == null)
            {
                return false;
            }

            if (regiaoAnterior.Texto == null || a.Texto == null)
            {
                return true;
            }

            return a.Localizacao.Top < regiaoAnterior.Localizacao.Bottom && a.Localizacao.Top >= Math.Abs(regiaoAnterior.Localizacao.Top - 0.5 * regiaoAnterior.Localizacao.Height);
        }

        private bool RetContemRet(Rectangle retanguloFiliacao, Rectangle localizacao)
        {
            if (localizacao.Top >= retanguloFiliacao.Top && localizacao.Top < retanguloFiliacao.Bottom && localizacao.Left >= retanguloFiliacao.Left && localizacao.Left < retanguloFiliacao.Right)
            {
                return true;
            }

            return false;
        }
    }
}
