namespace Veros.Paperless.Model.Servicos.Detalhe
{
    using System.Collections.Generic;
    using System.Linq;
    using Veros.Paperless.Model.Repositorios;
    using Veros.Paperless.Model.Entidades;

    public class CriarDetalheLoteServico : ICriarDetalheLoteServico
    {
        private readonly IRegraVioladaRepositorio regraVioladaRepositorio;
        
        public CriarDetalheLoteServico(
            IRegraVioladaRepositorio regraVioladaRepositorio)
        {
            this.regraVioladaRepositorio = regraVioladaRepositorio;
        }

        public DetalheLote Criar(Processo processo)
        {
            if (processo == null)
            {
                return null;
            }

            var detalhe = new DetalheLote
            {
                ProcessoId = processo.Id,
                Numero = processo.Conta,
                Identificacao = processo.Lote.Identificacao,
                EstaEmAprovacao = processo.Status == ProcessoStatus.AguardandoAprovacao
            };

            this.CarregarDocumentos(detalhe, processo.Documentos);
            this.CarregarRegras(detalhe, processo.Id);

            return detalhe;
        }

        private void CarregarRegras(DetalheLote detalhe, int processoId)
        {
            var regras = this.regraVioladaRepositorio.ObterRegrasVioladasParaDetalhe(processoId);

            if (regras == null)
            {
                return;
            }

            var documentoAprovacao = detalhe.Documentos.FirstOrDefault();

            foreach (var regra in regras.Where(x => x.EstaPendente()).OrderByDescending(x => x.Status))
            {
                if (regra.Documento == null)
                {
                    regra.Documento = new Documento
                    {
                        Id = documentoAprovacao.DocumentoId,
                        TipoDocumento = new TipoDocumento
                        {
                            Description = "Geral"
                        }
                    };
                }

                detalhe.Regras.Add(new DetalheRegra
                {
                    RegraId = regra.Id,
                    Cpf = regra.CpfParticipante,
                    Sequencial = regra.SequencialDoTitular,
                    Identificador = regra.Regra.Identificador,
                    Descricao = regra.Regra.Descricao,
                    DocumentoId = regra.Documento.Id,
                    TipoDeDocumento = regra.Documento.TipoDocumento.Description,
                    Violada = regra.Status != RegraVioladaStatus.Aprovada,
                    Status = regra.Status.Value,
                    Classificacao = regra.Regra.Classificacao
                });
            }
        }

        private void CarregarDocumentos(DetalheLote detalhe, IEnumerable<Documento> documentos)
        {
            foreach (var documento in documentos)
            {
                detalhe.Documentos.Add(new DetalheDocumento
                {
                    DocumentoId = documento.Id,
                    Cpf = documento.Cpf,
                    Sequencial = documento.SequenciaTitular,
                    Tipo = documento.TipoDocumento.Description,
                    Mestre = documento.TipoDocumento.Id == TipoDocumento.CodigoFichaDeCadastro,
                    IndicioDeFraude = documento.IndicioDeFraude
                });

                if (documento.TipoDocumento.IsDi)
                {
                    detalhe.DocumentoDiId = documento.Id;
                }

                if (documento.TipoDocumento.IsFoto)
                {
                    detalhe.DocumentoFotoId = documento.Id;
                }

                if (documento.TipoDocumento.IsComprovanteResidencia)
                {
                    detalhe.DocumentoComprovanteResidenciaId = documento.Id;
                }
            }
        }
    }
}