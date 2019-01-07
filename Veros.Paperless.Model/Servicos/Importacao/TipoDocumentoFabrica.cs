namespace Veros.Paperless.Model.Servicos.Importacao
{
    using System.Collections.Generic;
    using Entidades;
    using Framework;

    public class TipoDocumentoFabrica : ITipoDocumentoFabrica
    {
        private readonly IDictionary<CodigoTipoDocumentoDominio, TipoDocumento> tiposDocumentos;

        public TipoDocumentoFabrica()
        {
            this.tiposDocumentos = new Dictionary<CodigoTipoDocumentoDominio, TipoDocumento>();
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.Aposentadoria, new TipoDocumento { Id = TipoDocumento.CodigoRendaContraChequeInss });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ComprovanteAssalariado, new TipoDocumento { Id = TipoDocumento.CodigoRendaHolerite });

            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRenda, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina1, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina2, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina3, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina4, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina5, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina6, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina7, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina8, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina9, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ImpostoDeRendaPagina10, new TipoDocumento { Id = TipoDocumento.CodigoRendaImpostoRendaPf });

            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ComprovanteDeRenda, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.CartaDoRh, new TipoDocumento { Id = TipoDocumento.CodigoCartaDoRh });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.BoletoCondominio, new TipoDocumento { Id = TipoDocumento.CodigoResidenciaBoletoCondominio });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.Declaracaoresidencia, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });

            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda1, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda2, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda3, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda4, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda5, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda6, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda7, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda8, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda9, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.OutroComprovanteRenda10, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
            
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ContasConsumoConcessionarias, new TipoDocumento { Id = TipoDocumento.CodigoComprovanteDeResidencia });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ContasOrgaosPublicos, new TipoDocumento { Id = TipoDocumento.CodigoResidenciaIptu });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.ContratoDeLocacao, new TipoDocumento { Id = TipoDocumento.CodigoContratoLocacao });
            
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.DocumentoIdentificacaoFrente, new TipoDocumento { Id = TipoDocumento.CodigoDocumentoIdentificacao });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.DocumentoIdentificacaoVerso, new TipoDocumento { Id = TipoDocumento.CodigoDocumentoIdentificacao });
            
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.FotoPerfilFacebook, new TipoDocumento { Id = TipoDocumento.CodigoFotoFacebook });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.FotoPerfilLinkedin, new TipoDocumento { Id = TipoDocumento.CodigoFotoLinkedin });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.SelfieDiagonal, new TipoDocumento { Id = TipoDocumento.CodigoFotoLateral });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.SelfieFrente, new TipoDocumento { Id = TipoDocumento.CodigoFotoFrontal });
            
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.VideoLiveness, new TipoDocumento { Id = TipoDocumento.CodigoVideo });
            
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.Assinatura, new TipoDocumento { Id = TipoDocumento.CodigoAssinatura });
            this.tiposDocumentos.Add(CodigoTipoDocumentoDominio.NaoIdentificado, new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado });
        }

        public TipoDocumento Criar(CodigoTipoDocumentoDominio codigoTipoDocumentoDominio)
        {
            var tipoDocumento = this.tiposDocumentos[codigoTipoDocumentoDominio];

            return tipoDocumento;
        }
    }
}