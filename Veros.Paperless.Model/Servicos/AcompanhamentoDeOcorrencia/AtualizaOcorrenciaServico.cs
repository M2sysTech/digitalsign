namespace Veros.Paperless.Model.Servicos.AcompanhamentoDeOcorrencia
{
    using System;
    using System.Linq;
    using Entidades;
    using Framework;
    using Framework.Servicos;
    using Repositorios;

    public class AtualizaOcorrenciaServico : IAtualizaOcorrenciaServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IOcorrenciaRepositorio ocorrenciaRepositorio;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IOcorrenciaLogRepositorio ocorrenciaLogRepositorio;
        private readonly ITipoDocumentoRepositorio tipoDocumentoRepositorio;
        private readonly IGravaLogDaOcorrenciaServico gravaLogDaOcorrenciaServico;
        private readonly IGravaLogGenericoServico gravaLogGenericoServico;

        public AtualizaOcorrenciaServico(ISessaoDoUsuario userSession, 
            IOcorrenciaRepositorio ocorrenciaRepositorio, 
            IDocumentoRepositorio documentoRepositorio, 
            IOcorrenciaLogRepositorio ocorrenciaLogRepositorio, 
            ITipoDocumentoRepositorio tipoDocumentoRepositorio, 
            IGravaLogDaOcorrenciaServico gravaLogDaOcorrenciaServico, 
            IGravaLogGenericoServico gravaLogGenericoServico)
        {
            this.userSession = userSession;
            this.ocorrenciaRepositorio = ocorrenciaRepositorio;
            this.documentoRepositorio = documentoRepositorio;
            this.ocorrenciaLogRepositorio = ocorrenciaLogRepositorio;
            this.tipoDocumentoRepositorio = tipoDocumentoRepositorio;
            this.gravaLogDaOcorrenciaServico = gravaLogDaOcorrenciaServico;
            this.gravaLogGenericoServico = gravaLogGenericoServico;
        }

        public void Executar(Ocorrencia ocorrencia, string observacao, string novoTipoDocumento, int grupoId)
        {
            var ocorrenciaBanco = this.ocorrenciaRepositorio.ObterPorIdComDocumento(ocorrencia.Id);
            ocorrenciaBanco.Status = ocorrencia.Status;

            if (string.IsNullOrEmpty(observacao) == false)
            {
                this.gravaLogDaOcorrenciaServico.Executar(this.ObterAcao(ocorrencia), ocorrenciaBanco, observacao);    
            }

            this.ocorrenciaRepositorio.Salvar(ocorrenciaBanco);

            if (ocorrenciaBanco.TipoId != OcorrenciaTipo.TipoDocumentoNaoEncontrado ||
                novoTipoDocumento == null)
            {
                return;
            }

            this.GravarTipoDeDocumento(novoTipoDocumento, ocorrenciaBanco.Id, grupoId);

            if (ocorrenciaBanco.Status == OcorrenciaStatus.Finalizada)
            {
                this.AtualizarDocumento(ocorrenciaBanco.Documento);
            }
        }

        public bool VerificaTipoExistente(string novoTipoDocumento)
        {
            if (string.IsNullOrEmpty(novoTipoDocumento))
            {
                return false;
            }

            if (this.tipoDocumentoRepositorio.ObterPorDescricao(novoTipoDocumento.ToLower().RemoveAcentuacao()).Any())
            {
                return true;
            }

            return false;
        }

        private string ObterAcao(Ocorrencia ocorrenciaNova)
        {
            if (ocorrenciaNova.Status == OcorrenciaStatus.Finalizada)
            {
                return Ocorrencia.AcaoFinalizar;
            }

            if (ocorrenciaNova.Status == OcorrenciaStatus.AguardandoCef)
            {
                return Ocorrencia.AcaoAguardandoCef;
            }

            if (ocorrenciaNova.Status == OcorrenciaStatus.AguardandoM2)
            {
                return Ocorrencia.AcaoAguardandoM2;
            }

            return Ocorrencia.AcaoAdicionarObservacao;
        }

        private void AtualizarDocumento(Documento documento)
        {
            if (documento == null || documento.TipoDocumento.Id != TipoDocumento.CodigoAguardandoNovoTipo)
            {
                return;
            }

            documento.TipoDocumento = new TipoDocumento { Id = TipoDocumento.CodigoNaoIdentificado };
            documento.HoraInicio = null;
            this.documentoRepositorio.Salvar(documento);
        }
        
        private void GravarTipoDeDocumento(string novoTipoDocumento, int ocorrenciaId, int grupoId)
        {
            if (string.IsNullOrEmpty(novoTipoDocumento))
            {
                return;
            }

            var tipoDocumento = new TipoDocumento
            {
                Description = novoTipoDocumento.ToPascalCase().RemoveAcentuacao().Trim()
            };

            tipoDocumento.TypeDocCode = this.tipoDocumentoRepositorio.ObterUltimoTypedocCode() + 1;
            tipoDocumento.DataCriacao = DateTime.Now; 
            tipoDocumento.CodigoOcorrencia = ocorrenciaId;
            tipoDocumento.GrupoId = grupoId;

            this.tipoDocumentoRepositorio.Salvar(tipoDocumento);

            this.gravaLogGenericoServico.Executar(LogGenerico.AcaoNovoTipoDocumento, 
                tipoDocumento.Id, 
                "Novo documento criado. Ocorrencia #" + ocorrenciaId, 
                "Ocorrencia", 
                this.userSession.UsuarioAtual.Login);
        }
    }
}
