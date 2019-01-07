namespace Veros.Paperless.Model.Servicos
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Entidades;
    using Repositorios;

    /// <summary>
    /// TODO: Refatorar
    /// </summary>
    public class RegrasVioladasDoDocumentoParticipanteServico : IRegrasVioladasDoDocumentoParticipanteServico
    {
        private readonly IRegraVioladaRepositorio regravioladaRepositorio;
        private readonly IRegraImportadaRepositorio regraimportadaRepositorio;

        public RegrasVioladasDoDocumentoParticipanteServico(IRegraVioladaRepositorio regravioladaRepositorio, IRegraImportadaRepositorio regraimportadaRepositorio)
        {
            this.regravioladaRepositorio = regravioladaRepositorio;
            this.regraimportadaRepositorio = regraimportadaRepositorio;
        }

        public IList<RegraVioladaDocumentoParticipante> Obter(Processo processo, Participante participante)
        {
            IList<RegraVioladaDocumentoParticipante> violacoes = new List<RegraVioladaDocumentoParticipante>();

            foreach (var documento in ObterDocumentosDoCpf(processo, participante))
            {
                var regrasProcVioladas = this.regravioladaRepositorio.ObterRegrasVioladasParaExportacaoPorProcesso(processo.Id);

                var regrasProcImportadas = this.regraimportadaRepositorio.ObterRegrasImportadasPorDocumento(documento.Id);
                
                var existeRegrasMarcadaComVinculo = ExisteRegraMarcadaComVinculo(regrasProcVioladas,
                    regrasProcImportadas, documento);

                var existeRegrasGenericasVioladas = ExisteRegrasGenericasMarcadas(regrasProcVioladas, documento);

                foreach (var ocorreciasImportadasDocumento in regrasProcImportadas)
                {
                    var valorDoStatusDoDocumento = ObterStatusDoDocumento(documento,
                        existeRegrasMarcadaComVinculo, existeRegrasGenericasVioladas);

                    var totalRegrasVioladasComVinculo = regrasProcVioladas
                             .Count(x => x.Regra.Identificador == documento.ObterValorFinal(Campo.ReferenciaDeArquivoClassificacaoDaRendaDoParticipante)
                                 && x.Regra.Vinculo == ocorreciasImportadasDocumento.Vinculo
                                 && (x.Status == RegraVioladaStatus.Marcada)
                                 && x.Documento.Id == documento.Id);

                    var documentoComRegraGenericaViolada = VerificaRegraGenericaVioladaNoDocumento(regrasProcVioladas, documento, ocorreciasImportadasDocumento);

                    if (totalRegrasVioladasComVinculo >= 1 || documentoComRegraGenericaViolada)
                    {
                        violacoes.Add(new RegraVioladaDocumentoParticipante()
                        {
                            CodigoDoTipoDoDocumento = documento.ObterValorFinal(Campo.ReferenciaDeArquivoClassificacaoDaRendaDoParticipante),
                            CodigoDoDocumento = documento.ObterValorFinal(Campo.ReferenciaDeArquivoClassificacaoDaRendaDoParticipante),
                            StatusDoDocumento = valorDoStatusDoDocumento,
                            CodigoDaValidacao = ocorreciasImportadasDocumento.Vinculo,
                            IndicadorDaSelecaoDeValidacao = "X"
                        });
                    }
                    else
                    {
                        violacoes.Add(new RegraVioladaDocumentoParticipante()
                        {
                            CodigoDoTipoDoDocumento = documento.ObterValorFinal(Campo.ReferenciaDeArquivoClassificacaoDaRendaDoParticipante),
                            CodigoDoDocumento = documento.ObterValorFinal(Campo.ReferenciaDeArquivoClassificacaoDaRendaDoParticipante),
                            StatusDoDocumento = valorDoStatusDoDocumento,
                            CodigoDaValidacao = ocorreciasImportadasDocumento.Vinculo,
                            IndicadorDaSelecaoDeValidacao = string.Empty
                        });
                    }   
                }
            }

            return violacoes;
        }

        private static bool VerificaRegraGenericaVioladaNoDocumento(IList<RegraViolada> regrasProcVioladas, Documento documento, RegraImportada ocorreciasImportadas)
        {
            foreach (var regraVioladas in regrasProcVioladas)
            {
                if (regraVioladas.Regra.Identificador == "RGEN"
                    && (regraVioladas.Status == RegraVioladaStatus.Marcada)
                    && regraVioladas.Documento.Id == documento.Id 
                    && ocorreciasImportadas.Vinculo == regraVioladas.Vinculo.ToString(CultureInfo.InvariantCulture))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ExisteRegrasGenericasMarcadas(IList<RegraViolada> regrasProcVioladas, Documento documento)
        {
            var totalRegrasProcsGenericas = regrasProcVioladas.Count(x => x.Regra.Identificador == "RGEN"
                             && (x.Status == RegraVioladaStatus.Marcada)
                             && x.Documento.Id == documento.Id);

            if (totalRegrasProcsGenericas >= 1)
            {
                  return true;
            }

            return false;
        }

        private static bool ExisteRegraMarcadaComVinculo(IList<RegraViolada> regrasProcVioladas,
            IList<RegraImportada> regrasProcImportadas,
            Documento documento)
        {
            foreach (var ocorreciasImportadasDocumento in regrasProcImportadas)
            {
                var totalRegrasVioladasComVinculo = regrasProcVioladas
                         .Count(x => x.Regra.Identificador == documento.ObterValorFinal(Campo.ReferenciaDeArquivoClassificacaoDaRendaDoParticipante)
                             && x.Regra.Vinculo == ocorreciasImportadasDocumento.Vinculo
                             && (x.Status == RegraVioladaStatus.Marcada)
                             && x.Documento.Id == documento.Id);

                if (totalRegrasVioladasComVinculo >= 1)
                {
                    return true;
                }
            }

            return false;
        }

        private static string ObterStatusDoDocumento(Documento documento,
            bool existeRegrasMarcadaComVinculo, bool existeRegrasGenericasMarcadas)
        {
            if (string.IsNullOrEmpty(documento.IndicioDeFraude) && (existeRegrasMarcadaComVinculo || existeRegrasGenericasMarcadas))
            {
                return "02";
            }

            if (string.IsNullOrEmpty(documento.IndicioDeFraude) == false && (existeRegrasMarcadaComVinculo || existeRegrasGenericasMarcadas))
            {
                return "04";
            }

            if (string.IsNullOrEmpty(documento.IndicioDeFraude) && (existeRegrasMarcadaComVinculo == false && existeRegrasGenericasMarcadas == false))
            {
                return "01";
            }

            if (string.IsNullOrEmpty(documento.IndicioDeFraude) == false && (existeRegrasMarcadaComVinculo == false && existeRegrasGenericasMarcadas == false))
            {
                return "03";
            }

            return "01";
        }

        private static IEnumerable<Documento> ObterDocumentosDoCpf(Processo processo, Participante participante)
        {
            return processo.Documentos.Where(x => x.ObterValorFinal(Campo.ReferenciaDeArquivoCpf) == participante.CpfDoTitularOuRepresentante  && x.ObterValorFinal(Campo.ReferenciaDeArquivoCidadeDaResidenciaDoParticipante) == participante.SequenciaDeTitularidade);
        }
    }
}
