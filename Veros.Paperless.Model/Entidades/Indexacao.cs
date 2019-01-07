namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Modelo;
    using Veros.Framework;
    using Veros.Paperless.Model.Servicos.Comparacao;
    using Veros.Paperless.Model.Servicos.Batimento;
    using Veros.Paperless.Model.Servicos.Batimento.Experimental;

    /// <summary>
    /// TODO: refatorar, retirar responsabilidades que nao é de indexacao
    /// </summary>
    [Serializable]
    public class Indexacao : Entidade
    {
        private readonly ICriadorDeComparador criadorDeComparador;
        
        public Indexacao()
        {
            this.criadorDeComparador = new CriadorDeComparador();
        }

        /// <summary>
        /// TODO: retirar esse construtor
        /// </summary>
        /// <param name="criadorDeComparador"></param>
        public Indexacao(ICriadorDeComparador criadorDeComparador)
        {
            this.criadorDeComparador = criadorDeComparador;
        }

        public virtual string PrimeiroValor
        {
            get;
            set;
        }

        public virtual string SegundoValor
        {
            get;
            set;
        }

        public virtual string ValorFinal
        {
            get;
            set;
        }

        public virtual Documento Documento
        {
            get;
            set;
        }

        public virtual Campo Campo
        {
            get;
            set;
        }

        public virtual int? UsuarioPrimeiroValor
        {
            get;
            set;
        }

        public virtual DateTime? DataPrimeiraDigitacao
        {
            get;
            set;
        }

        public virtual int UsuarioSegundoValor
        {
            get;
            set;
        }

        public virtual DateTime? DataSegundaDigitacao
        {
            get;
            set;
        }

        public virtual ValorUtilizadoParaValorFinal ValorUtilizadoParaValorFinal
        {
            get; 
            set;
        }

        public virtual bool OcrComplementou
        {
            get;
            set;
        }

        public virtual bool ValidacaoComplementou
        {
            get;
            set;
        }

        public virtual bool ValorRecuperado
        {
            get;
            set;
        }

        public virtual string ValorFormatado
        {
            get; 
            set;
        }

        public virtual bool ValorFinalValido()
        {
            return this.ValorFinal != "#" && 
                this.ValorFinal != "?" && 
                this.ValorFinal != "!" && 
                string.IsNullOrEmpty(this.ValorFinal) == false &&
                string.IsNullOrWhiteSpace(this.ValorFinal) == false;
        }

        public virtual bool Valor1UtilizadoNoValorFinal()
        {
            return this.ValorUtilizadoParaValorFinal == ValorUtilizadoParaValorFinal.PrimeiroValor 
                || this.PrimeiroValor == this.ValorFinal;
        }

        public virtual bool Valor2UtilizadoNoValorFinal()
        {
            return this.ValorUtilizadoParaValorFinal == ValorUtilizadoParaValorFinal.SegundoValor
                || this.SegundoValor == this.ValorFinal;
        }

        public virtual bool BateCom(string primeiroValor)
        {
            if (string.IsNullOrWhiteSpace(primeiroValor))
            {
                return false;
            }

            if (this.Campo.TipoCampo == TipoCampo.Nenhum)
            {
                return this.criadorDeComparador.Cria(this.Campo.TipoDado).SaoIguais(this.SegundoValor, primeiroValor);
            }
            
            return this.criadorDeComparador.Cria(this.Campo.TipoCampo).SaoIguais(this.SegundoValor, primeiroValor);
        }

        public virtual bool BateComFullText(IList<dynamic> palavrasReconhecidas)
        {
            if (palavrasReconhecidas == null)
            {
                return false;
            }

            if (palavrasReconhecidas.Count == 0)
            {
                return false;
            }

            var nomeCompleto = this.SegundoValor;
            if (string.IsNullOrEmpty(nomeCompleto))
            {
                return false;
            }
            
            nomeCompleto = nomeCompleto.RemoveAcentuacao().ToLower().Trim();
            
            var listaNomes = nomeCompleto.Split(' ').ToList();

            var listaNomesPrimeiroValor = new List<string>();

            if (this.EhCampoNomeDaMae() || this.EhCampoNomeDoPai())
            {
                listaNomesPrimeiroValor.Add(listaNomes.First());
                listaNomesPrimeiroValor.Add(listaNomes.Last());
            }
            else
            {
                listaNomesPrimeiroValor = listaNomes;
            }

            var encontrou = false;
            var ultimaPosicaoEncontrada = 0;
            var contadorMatch = 0;
            var posInicial = 0;

            foreach (var nomeAtual in listaNomesPrimeiroValor)
            {
                for (int i = posInicial; i < palavrasReconhecidas.Count; i++)
                {
                    var itemAtual = palavrasReconhecidas[i].Texto.ToLower();
                    ////primeiro Nome
                    if (!encontrou) 
                    {
                        if (itemAtual.Equals(nomeAtual))
                        {
                            encontrou = true;
                            ultimaPosicaoEncontrada = i;
                            contadorMatch++;
                            posInicial = i + 1;
                            break;
                        }
                    }
                    else
                    {
                        //// segundo nome em diante
                        if (itemAtual.Equals(nomeAtual))
                        {
                            if (this.EhCampoNomeDaMae() == false && this.EhCampoNomeDoPai() == false)
                            {
                                if (i - 1 != ultimaPosicaoEncontrada)
                                {
                                    return false;
                                }
                            }

                            ultimaPosicaoEncontrada = i;
                            contadorMatch++;
                            posInicial = i + 1;
                            break;
                        }
                    }
                }

                if (!encontrou)
                {
                    return false;
                }
            }

            if (encontrou && contadorMatch == listaNomesPrimeiroValor.Count)
            {
                ////checagem de palavra extra apos o fim do nome, na mesma linha, numa distância de até 3 caracteres do último nome
                var ultimoNome = palavrasReconhecidas[ultimaPosicaoEncontrada];
                if (palavrasReconhecidas.Count - 1 > ultimaPosicaoEncontrada)
                {
                    var palavraSeguinte = palavrasReconhecidas[ultimaPosicaoEncontrada + 1];
                    int espacamentoMaximo;
                    if (ultimoNome.Texto.Length > 0)
                    {
                        espacamentoMaximo = Math.Abs(ultimoNome.Localizacao.Width / ultimoNome.Texto.Length) * 3;
                    }
                    else
                    {
                        espacamentoMaximo = 50;
                    }

                    if ((palavraSeguinte.Localizacao.Top >= ultimoNome.Localizacao.Top && palavraSeguinte.Localizacao.Top <= ultimoNome.Localizacao.Bottom)
                        || (palavraSeguinte.Localizacao.Bottom >= ultimoNome.Localizacao.Top && palavraSeguinte.Localizacao.Bottom <= ultimoNome.Localizacao.Bottom))
                    {
                        if (palavraSeguinte.Localizacao.Left < ultimoNome.Localizacao.Right + espacamentoMaximo)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public virtual bool EstaBatido()
        {
            if (this.Campo.ParaValidacao == false && this.Campo.DuplaDigitacao == false)
            {
                Log.Application.InfoFormat(@"Campo #{0} não pode ser validado.
Campo está configurado para não ser validado e será considerado com campo batido.
Para realizar o batimento do campo deve-se marcar o coluna TDCAMPOS_VALIDACAO para [S]",
                    this.Campo.Description);

                return true;
            }

            if (string.IsNullOrWhiteSpace(this.ValorFinal) == false & string.IsNullOrEmpty(this.ValorFinal) == false)
            {
                Log.Application.InfoFormat(@"Campo #{0} já está com valor final preenchido, logo será considerado como batido",
                    this.Campo.Description);

                return true;
            }

            if (this.PrimeiroValor == "?" || this.PrimeiroValor == "#")
            {
                return false;
            }

            return this.BateCom(this.PrimeiroValor);
        }

        public virtual string ObterValor()
        {
            if (string.IsNullOrEmpty(this.ValorFinal) == false)
            {
                return this.ValorFinal;
            }

            if (string.IsNullOrEmpty(this.PrimeiroValor) == false)
            {
                return this.PrimeiroValor;
            }

            if (string.IsNullOrEmpty(this.SegundoValor) == false)
            {
                return this.SegundoValor;
            }

            return string.Empty;
        }

        public virtual string ObterValor(ColunaDestino coluna)
        {
            switch (coluna)
            {
                case ColunaDestino.Valor1:
                    return this.PrimeiroValor;

                case ColunaDestino.Valor2:
                    return this.SegundoValor;

                case ColunaDestino.ValorFinal:
                    return this.ValorFinal;
            }

            return string.Empty;
        }

        public virtual string ObterValor(string coluna)
        {
            if (coluna.ToUpper().Equals("MDOCDADOS_VALOR1"))
            {
                return this.ObterValor(ColunaDestino.Valor1);
            }

            if (coluna.ToUpper().Equals("MDOCDADOS_VALOR2"))
            {
                return this.ObterValor(ColunaDestino.Valor2);
            }

            if (coluna.ToUpper().Equals("MDOCDADOS_VALOROK"))
            {
                return this.ObterValor(ColunaDestino.ValorFinal);
            }

            return string.Empty;
        }

        public virtual ValorUtilizadoParaValorFinal ObterValorUtilizado(ColunaDestino coluna)
        {
            switch (coluna)
            {
                case ColunaDestino.Valor1:
                    return ValorUtilizadoParaValorFinal.PrimeiroValor;
                case ColunaDestino.Valor2:
                    return ValorUtilizadoParaValorFinal.SegundoValor;
            }

            return ValorUtilizadoParaValorFinal.NaoDefinido;
        }

        public virtual void AtribuirValor(ColunaDestino coluna, string valor)
        {
            switch (coluna)
            {
                case ColunaDestino.Valor1:
                    this.PrimeiroValor = valor;
                    break;

                case ColunaDestino.Valor2:
                    this.SegundoValor = valor;
                    break;

                case ColunaDestino.ValorFinal:
                    this.ValorFinal = valor;
                    break;
            }
        }

        public virtual bool EstaDigitado()
        {
            if (this.Campo.Digitavel == false)
            {
                return true;
            }

            if (string.IsNullOrEmpty(this.PrimeiroValor))
            {
                return false;
            }

            if (this.Campo.DuplaDigitacao && string.IsNullOrEmpty(this.SegundoValor))
            {
                return false;
            }

            return true;
        }

        public virtual string ValorFinalTratado()
        {
            if (string.IsNullOrEmpty(this.ValorFinal))
            {
                return string.Empty;
            }

            return this.ValorFinal.Trim();
        }

        public virtual string PrimeiroValorTratado()
        {
            if (string.IsNullOrEmpty(this.PrimeiroValor))
            {
                return string.Empty;
            }

            return this.PrimeiroValor.Trim();
        }

        public virtual string SegundoValorTratado()
        {
            if (string.IsNullOrEmpty(this.SegundoValor))
            {
                return string.Empty;
            }

            return this.SegundoValor.Trim();
        }

        public virtual bool ValorFinalFoiAlterado()
        {
            if (string.IsNullOrEmpty(this.ValorFinal) || this.ValorFinal == "#" || this.ValorFinal == "?")
            {
                return false;
            }

            return this.ValorFinalTratado().ToUpper() != this.SegundoValorTratado().ToUpper();
        }

        public virtual bool EhCampoNomeDaMae()
        {
            return this.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoNomeMaeCliente;
        }

        public virtual bool EhCampoNomeDoPai()
        {
            return this.Campo.ReferenciaArquivo == Campo.ReferenciaDeArquivoNomePaiCliente;
        }

        public virtual string ObterValorParaBatimento()
        {
            switch (BaterDocumento.BatimentoCom)
            {
                case BatimentoDo.ValorFinal:
                    return this.ValorFinal;

                default:
                    return this.SegundoValor;
            }
        }

        public virtual string ObterValorFinalValido()
        {
            return this.ValorFinalValido() ?
                this.ValorFinal :
                this.SegundoValor;
        }
    }
}
