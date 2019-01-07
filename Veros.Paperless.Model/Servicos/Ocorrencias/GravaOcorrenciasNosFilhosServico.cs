namespace Veros.Paperless.Model.Servicos.Ocorrencias
{
    using Entidades;
    using Framework;
    using Repositorios;

    public class GravaOcorrenciasNosFilhosServico : IGravaOcorrenciasNosFilhosServico
    {
        private readonly IOcorrenciaRepositorio ocorrenciaRepositorio;
        private readonly IDossieEsperadoRepositorio dossieEsperadoRepositorio;
        private readonly IOcorrenciaTipoRepositorio ocorrenciaTipoRepositorio;
        private readonly IGravaLogDoDossieEsperadoServico gravaLogDoDossieEsperadoServico;

        public GravaOcorrenciasNosFilhosServico(IOcorrenciaRepositorio ocorrenciaRepositorio, 
            IDossieEsperadoRepositorio dossieEsperadoRepositorio, 
            IOcorrenciaTipoRepositorio ocorrenciaTipoRepositorio, 
            IGravaLogDoDossieEsperadoServico gravaLogDoDossieEsperadoServico)
        {
            this.ocorrenciaRepositorio = ocorrenciaRepositorio;
            this.dossieEsperadoRepositorio = dossieEsperadoRepositorio;
            this.ocorrenciaTipoRepositorio = ocorrenciaTipoRepositorio;
            this.gravaLogDoDossieEsperadoServico = gravaLogDoDossieEsperadoServico;
        }

        public void Executar(Ocorrencia ocorrencia)
        {
            if (ocorrencia == null || ocorrencia.Tipo == null)
            {
                return;
            }
            
            var tipoOcorrencia = this.ocorrenciaTipoRepositorio.ObterPorId(ocorrencia.TipoId);

            if (tipoOcorrencia == null || tipoOcorrencia.TipoOcorrenciaParaFilhos < 1)
            {
                return;
            }

            if (ocorrencia.PacoteId < 1 || ocorrencia.DossieEsperadoId > 0)
            {
                Log.Application.InfoFormat("Não foi gravado ocorrência para os filhos pois a ocorrência atual possui dossiê esperado. Ocorrência:{0}", ocorrencia.Id);
                return;
            }

            var dossies = this.dossieEsperadoRepositorio.ObterPorPacote(ocorrencia.Pacote);

            foreach (var dossieEsperado in dossies)
            {
                var ocorrenciaNova = this.GerarOcorrencia(dossieEsperado, ocorrencia);
                this.ocorrenciaRepositorio.Salvar(ocorrenciaNova);

                this.gravaLogDoDossieEsperadoServico.Executar(LogDossieEsperado.AcaoAdicionarLogOcorrenciaDossie, ocorrenciaNova.DossieEsperado,
                            "Registrado ocorrência para o dossie [" + ocorrenciaNova.DossieEsperadoId + "]");    
            }
        }

        private Ocorrencia GerarOcorrencia(DossieEsperado dossieEsperado, Ocorrencia ocorrencia)
        {
            return new Ocorrencia
            {
                OcorrenciaPaiId = ocorrencia.Id,
                DataRegistro = ocorrencia.DataRegistro,
                Status = ocorrencia.Status,
                Tipo = ocorrencia.Tipo,
                Pacote = ocorrencia.Pacote,
                DossieEsperado = dossieEsperado,
                Documento = ocorrencia.Documento,
                Lote = dossieEsperado.Lote(),
                UsuarioRegistro = ocorrencia.UsuarioRegistro,
                UsuarioResponsavel = ocorrencia.UsuarioResponsavel,
                Observacao = ocorrencia.Observacao,
                GrupoId = ocorrencia.GrupoId
            };
        }
    }
}
