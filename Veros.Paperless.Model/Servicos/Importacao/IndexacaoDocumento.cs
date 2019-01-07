namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System;
    using Entidades;
    using Framework;
    using Ph3;
    using Repositorios;
    using Storages;

    public class IndexacaoDocumento
    {
        private readonly IConsultaPfStorage consultaPfStorage;
        private readonly IIndexacaoFabrica indexacaoFabrica;
        private readonly IIndexacaoRepositorio indexacaoRepositorio;
        
        public IndexacaoDocumento(
            IConsultaPfStorage consultaPfStorage,
            IIndexacaoFabrica indexacaoFabrica,
            IIndexacaoRepositorio indexacaoRepositorio)
        {
            this.consultaPfStorage = consultaPfStorage;
            this.indexacaoFabrica = indexacaoFabrica;
            this.indexacaoRepositorio = indexacaoRepositorio;
        }

        public void Executar(Documento documento, string cpf)
        {
            var consultaPf = this.consultaPfStorage.Obter(cpf);
            
            if (consultaPf == null)
            {
                Log.Application.Warn("Sem informacoes no redis para cpf #" + cpf);
                return;
            }

            switch (documento.TipoDocumento.Id)
            {
                case TipoDocumento.CodigoRg:
                case TipoDocumento.CodigoCnh:
                case TipoDocumento.CodigoDiOutro:
                    this.InserirIndexacaoDi(documento, consultaPf);
                    break;

                case TipoDocumento.CodigoComprovanteDeResidencia:
                    this.InserirIndexacaoResidencia(documento, consultaPf);
                    break;

                case TipoDocumento.CodigoFichaDeCadastro:
                    this.InserirIndexacaoFichaCadastro(documento, consultaPf);
                    break;
            }
        }

        private void InserirIndexacaoFichaCadastro(Documento documento, ConsultaPf consultaPf)
        {
            Log.Application.Info("Inserindo informações na ficha de cadastro #" + documento.Id);

            ////Cobrança Terceiro
            var indexacaoDocumento = this.indexacaoFabrica
               .Criar(documento, Campo.ReferenciaDeArquivoCobrancaTerceira, consultaPf.SituacaoCobranca);
            this.AdicionarIndexacao(indexacaoDocumento);

            ////Situacao Receita
            indexacaoDocumento = this.indexacaoFabrica
               .Criar(documento, Campo.ReferenciaDeArquivoSituacaoReceita, consultaPf.DadosCadastrais.SituacaoReceita);
            this.AdicionarIndexacao(indexacaoDocumento);

            ////Data Obito
             DateTime dataObito;
             if (DateTime.TryParse(consultaPf.DadosCadastrais.DataObito, out dataObito))
             {
                 indexacaoDocumento = this.indexacaoFabrica
                     .Criar(documento, Campo.ReferenciaDeArquivoDataObito, dataObito.ToString("ddMMyyyy"));
                 this.AdicionarIndexacao(indexacaoDocumento);
             }

            ////ScoreCobrança
            indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoScoreCobranca, consultaPf.Score);
            this.AdicionarIndexacao(indexacaoDocumento);

            ////ScoreDeEnderco
            if (consultaPf.Enderecos.NaoTemConteudo() == false)
            {
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoScoreEndereco, consultaPf.Enderecos.Score);
                this.AdicionarIndexacao(indexacaoDocumento);
            }

            ////Signo
            indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoSigno, consultaPf.DadosCadastrais.Signo);
            this.AdicionarIndexacao(indexacaoDocumento);

            ////ppe
            indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoPpe, consultaPf.DadosCadastrais.Ppe);
            this.AdicionarIndexacao(indexacaoDocumento);

            ////ccf
            indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoChequeSemFundo, consultaPf.Ccfs.Ccf.Count.ToString());
            this.AdicionarIndexacao(indexacaoDocumento);
        }

        private void InserirIndexacaoResidencia(Documento documento, ConsultaPf consultaPf)
        {
            Log.Application.Info("Inserindo informações do doc de residencia #" + documento.Id);

            ////Nome
            var indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoNomeTitular, consultaPf.DadosCadastrais.Nome);
            this.AdicionarIndexacao(indexacaoDocumento);

            if (consultaPf.Enderecos != null)
            {
                ////Bairro
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoBairroDaResidenciaDoParticipante, consultaPf.Enderecos.Bairro);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////Cep
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoCepDaResidenciaDoParticipante, consultaPf.Enderecos.Cep);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////cidade
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoCidadeDaResidenciaDoParticipante, consultaPf.Enderecos.Cidade);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////Complemento
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoComplementoDaResidenciaDoParticipante, consultaPf.Enderecos.Complemento);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////Logradouro
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoLogradouroDaResidenciaDoParticipante, consultaPf.Enderecos.Logradouro);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////Numero
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoNumeroDaResidenciaDoParticipante, consultaPf.Enderecos.Numero);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////Uf
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoEstadoDaResidenciaDoParticipante, consultaPf.Enderecos.Uf);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////Tipo
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoTipoLogradouroResidencial, consultaPf.Enderecos.Tipo);
                this.AdicionarIndexacao(indexacaoDocumento);
            }

            ////TelefoneCelular
            if (consultaPf.Telefones != null)
            {
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoTelefoneCelular, consultaPf.Telefones.Telefone);
                this.AdicionarIndexacao(indexacaoDocumento);

                ////TelefoneFixo
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoTelefoneFixo, consultaPf.Telefones.Telefone);
                this.AdicionarIndexacao(indexacaoDocumento);
            }
        }

        private void InserirIndexacaoDi(Documento documento, ConsultaPf consultaPf)
        {
            Log.Application.Info("Inserindo informações do doc de identificacao #" + documento.Id);

            var indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoNomeTitular, consultaPf.DadosCadastrais.Nome);
            this.AdicionarIndexacao(indexacaoDocumento);

            DateTime dataNascimento;
            if (DateTime.TryParse(consultaPf.DadosCadastrais.DataNascimento, out dataNascimento))
            {
                indexacaoDocumento = this.indexacaoFabrica
                    .Criar(documento, Campo.ReferenciaDeArquivoDataDeNascimentoDoParticipante, dataNascimento.ToString("ddMMyyyy"));
                this.AdicionarIndexacao(indexacaoDocumento);
            }

            indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoNomeMaeCliente, consultaPf.DadosCadastrais.NomeMae);
            this.AdicionarIndexacao(indexacaoDocumento);

            indexacaoDocumento = this.indexacaoFabrica
                .Criar(documento, Campo.ReferenciaDeArquivoNumeroRegistroDocumentoIdentificacao, consultaPf.DadosCadastrais.RegistroGeral);
            this.AdicionarIndexacao(indexacaoDocumento);
        }

        private void AdicionarIndexacao(Indexacao indexacaoDocumento)
        {
            if (indexacaoDocumento.NaoTemConteudo() == false)
            {
                Log.Application.InfoFormat(
                    "Inserindo segundo valor do campo {0}:{1}",
                    indexacaoDocumento.Campo.Id,
                    indexacaoDocumento.SegundoValor);

                this.indexacaoRepositorio.Salvar(indexacaoDocumento);
            }
        }
    }
}