namespace Veros.Paperless.Model.Servicos.Batimento
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Entidades;

    public class OcorrenciaCampoDataNaCnh
    {
        public OcorrenciaCampoDataNaCnh()
        {
            this.ListagemRegioesEncontradas = new List<dynamic>();
        }

        public List<dynamic> ListagemRegioesEncontradas
        {
            get;
            set;
        }

        public int QuantidadeDatasEncontradas
        {
            get
            {
                return this.ListagemRegioesEncontradas.Count;
            }
        }

        public int PosicaoPrimeiraDataNaListaOriginal
        {
            get;
            set;
        }

        public int PosicaoUltimaDataNaListaOriginal
        {
            get;
            set;
        }

        public dynamic ObterPosicao(int i)
        {
            if (this.ListagemRegioesEncontradas == null)
            {
                return null;
            }

            if (this.ListagemRegioesEncontradas.Count <= i)
            {
                return null;
            }

            return this.ListagemRegioesEncontradas[i];
        }

        //// procura até a terceira ocorrencia de campo data numa lista de palavras
        ////deve encontrar as 3 datas para ter certeza: data nascimento, data validade e data habilitação. 
        //// Atualização: 2 datas já bastam: nascimento e qualquer uma das outras duas. 
        public void LocalizarDatas(IList<PalavraReconhecida> palavras)
        {
            const string RegexData = "([0-9]{2})([/])(0[1-9]|10|11|12)([/])([0-9]{4})";
            this.PosicaoPrimeiraDataNaListaOriginal = 0;
            this.PosicaoUltimaDataNaListaOriginal = palavras.Count - 1;
            for (var i = 0; i < palavras.Count; i++)
            {
                var dynamic = palavras[i];
                var textoAux = dynamic.Texto.Replace("O", "0");
                textoAux = dynamic.Texto.Replace("Q", "0");
                textoAux = textoAux.Replace("l", "1");
                textoAux = textoAux.Replace("S", "5");
                textoAux = textoAux.Replace("T", "7");
                if (Regex.IsMatch(textoAux, RegexData))
                {
                    switch (this.ListagemRegioesEncontradas.Count)
                    {
                        case 0:
                            ////this.ListagemRegioesEncontradas.Add(new dynamic()
                            ////{
                            ////    Localizacao = dynamic.Localizacao,
                            ////    Texto = dynamic.Texto
                            ////});
                            ////this.PosicaoPrimeiraDataNaListaOriginal = i;
                            break;
                        case 1:
                            ////this.ListagemRegioesEncontradas.Add(new dynamic()
                            ////{
                            ////    Localizacao = dynamic.Localizacao,
                            ////    Texto = dynamic.Texto
                            ////});
                            ////this.PosicaoUltimaDataNaListaOriginal = i;
                            break;
                        case 2:
                            ////if (dynamic.Localizacao.Top >= this.ListagemRegioesEncontradas[1].Localizacao.Top - 10 && dynamic.Localizacao.Top < this.ListagemRegioesEncontradas[1].Localizacao.Top + 10)
                            ////{
                            ////    this.ListagemRegioesEncontradas.Add(new dynamic()
                            ////    {
                            ////        Localizacao = dynamic.Localizacao,
                            ////        Texto = dynamic.Texto
                            ////    });
                            ////    break;
                            ////}

                            break;
                    }

                    if (this.ListagemRegioesEncontradas.Count == 3)
                    {
                        break;
                    }
                }
            }            
        }

        public bool PossuiDistanciaMinimaEntreDatas()
        {
            if (this.ListagemRegioesEncontradas == null)
            {
                return false;
            }

            if (this.ListagemRegioesEncontradas.Count < 2)
            {
                return false;
            }

            return Math.Abs(this.ListagemRegioesEncontradas[0].Localizacao.Top - this.ListagemRegioesEncontradas[1].Localizacao.Top) > 450;
        }
    }
}
