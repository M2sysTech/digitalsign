namespace Veros.Paperless.Infra.Consultas
{
    using System.Collections;
    using System.Collections.Generic;
    using Framework;
    using Model.Ph3;
    using Model.Servicos.Campos;

    public class ConsultaPfFabrica
    {
        public static ConsultaPf Criar(object[] resultado)
        {
            var consultaPf = new ConsultaPf();

            Log.Application.Info("colunas encontradas " + resultado.Length);

            consultaPf.DadosCadastrais = CriarDadosCadastrais(resultado, consultaPf);
            consultaPf.Score = resultado[29].ToString();
            consultaPf.Renda = CriarDadosDeRenda(resultado, consultaPf);
            consultaPf.ReferenciaBancaria = CriaDadosReferenciaBancaria(resultado, consultaPf);
            consultaPf.PossuiSociedades = "N";
            consultaPf.PossuiVeiculos = resultado[6].ToString();
            consultaPf.PossuiProtesto = "N";
            consultaPf.SituacaoCobranca = resultado[7].ToString();
            consultaPf.Telefones = CriaDadosTelefones(resultado, consultaPf);
            ////consultaPf.Enderecos = CriaDadosEnderecos(resultado, consultaPf);
            consultaPf.Emails = CriaDadosEmails(resultado, consultaPf);
            consultaPf.PessoasContato = new PessoasContato();
            consultaPf.AgregadosFamiliares = new AgregadosFamiliares();
            consultaPf.Veiculos = new Veiculos();
            consultaPf.Ccfs = CriaDadosCcf(resultado, consultaPf);
            consultaPf.HistoricoProfissional = new HistoricoProfissional();

            return consultaPf;
        }

        private static Ccfs CriaDadosCcf(object[] resultado, ConsultaPf consultaPf)
        {
            consultaPf.Ccfs = new Ccfs
            {
                Ccf = new List<Ccf>
                {
                    new Ccf { Quantidade = "3" },
                    new Ccf { Quantidade = "1" }
                }
            };

            return consultaPf.Ccfs;
        }

        private static Emails CriaDadosEmails(IList resultado, ConsultaPf consultaPf)
        {
            consultaPf.Emails = new Emails
            {
                Email = resultado[66].ToString()
            };

            return consultaPf.Emails;
        }

        private static Enderecos CriaDadosEnderecos(IList resultado, ConsultaPf consultaPf)
        {
            ////consultaPf.Enderecos = new Enderecos
            ////{
            ////    Bairro = resultado[20].ToString(),
            ////    Numero = resultado[27].ToString(),
            ////    Ranking = resultado[28].ToString(),
            ////    Cep = resultado[21].ToString(),
            ////    Cidade = resultado[22].ToString(),
            ////    Complemento = resultado[23].ToString(),
            ////    Logradouro = resultado[25].ToString(),
            ////    Score = resultado[29].ToString(),
            ////    Tipo = resultado[30].ToString(),
            ////    Titulo = resultado[31].ToString(),
            ////    Uf = resultado[32].ToString()
            ////};

            return null;  ////consultaPf.Enderecos;
        }

        private static Telefones CriaDadosTelefones(IList resultado, ConsultaPf consultaPf)
        {
            var telefone = string.Format(
                "({0}) {1}",
                resultado[33],
                resultado[34]);

            consultaPf.Telefones = new Telefones
            {
                Ddd = resultado[33].ToString(),
                Numero = resultado[34].ToString(),
                Ranking = resultado[35].ToString(),
                Telefone = telefone
            };

            return consultaPf.Telefones;
        }

        private static ReferenciaBancaria CriaDadosReferenciaBancaria(IList resultado, ConsultaPf consultaPf)
        {
            consultaPf.ReferenciaBancaria = new ReferenciaBancaria
            {
                Agencia = "1347",
                CodigoBanco = "033",
                Tipo = "tipo"
            };

            return consultaPf.ReferenciaBancaria;
        }

        private static Renda CriarDadosDeRenda(IList resultado, ConsultaPf consultaPf)
        {
            consultaPf.Renda = new Renda
            {
                FaixaInicial = "1000",
                FaixaFinal = "2000",
            };

            return consultaPf.Renda;
        }

        private static DadosCadastrais CriarDadosCadastrais(IList resultado, ConsultaPf consultaPf)
        {
            consultaPf.DadosCadastrais = new DadosCadastrais();
            consultaPf.DadosCadastrais.CPf = resultado[0].ToString();
            consultaPf.DadosCadastrais.Nome = resultado[2].ToString();
            consultaPf.DadosCadastrais.NomeUltimo = Nome.AbreviarExcetoPrimeiroEUltimo(resultado[2].ToString());
            consultaPf.DadosCadastrais.Sexo = resultado[19].ToString();
            consultaPf.DadosCadastrais.TituloEleitor = resultado[31].ToString();
            consultaPf.DadosCadastrais.RegistroGeral = resultado[1].ToString();
            consultaPf.DadosCadastrais.Escolaridade = resultado[11].ToString();
            consultaPf.DadosCadastrais.NumeroDependentes = "6";
            consultaPf.DadosCadastrais.Cor = "pardo";
            consultaPf.DadosCadastrais.CpfMae = "75487896545";
            consultaPf.DadosCadastrais.NomeMae = resultado[14].ToString();
            consultaPf.DadosCadastrais.DataNascimento = resultado[3].ToString();
            consultaPf.DadosCadastrais.Idade = resultado[13].ToString();
            consultaPf.DadosCadastrais.EstadoCivil = resultado[12].ToString();
            consultaPf.DadosCadastrais.Signo = "Touro";
            consultaPf.DadosCadastrais.SituacaoReceita = "Regular";
            consultaPf.DadosCadastrais.DataObito = resultado[15].ToString();
            consultaPf.DadosCadastrais.CnpjEmpregador = "00.111.222/0001-33";
            consultaPf.DadosCadastrais.Cbo = resultado[9].ToString();
            consultaPf.DadosCadastrais.CboDescricao = resultado[9].ToString();
            consultaPf.DadosCadastrais.RendaPresumida = resultado[18].ToString();
            consultaPf.DadosCadastrais.Ppe = resultado[16].ToString();

            return consultaPf.DadosCadastrais;
        }
    }
}