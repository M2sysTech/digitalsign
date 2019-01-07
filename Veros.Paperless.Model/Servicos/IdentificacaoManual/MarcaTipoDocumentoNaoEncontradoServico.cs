namespace Veros.Paperless.Model.Servicos.IdentificacaoManual
{
    using System;
    using Entidades;
    using Framework.Servicos;
    using Repositorios;

    public class MarcaTipoDocumentoNaoEncontradoServico : IMarcaTipoDocumentoNaoEncontradoServico
    {
        private readonly ISessaoDoUsuario userSession;
        private readonly IDocumentoRepositorio documentoRepositorio;
        private readonly IReclassificaDocumentoSeparadoServico reclassificaDocumentoSeparadoServico;
        private readonly IOcorrenciaRepositorio ocorrenciaRepositorio;

        public MarcaTipoDocumentoNaoEncontradoServico(
            ISessaoDoUsuario userSession,
            IDocumentoRepositorio documentoRepositorio,
            IReclassificaDocumentoSeparadoServico reclassificaDocumentoSeparadoServico, 
            IOcorrenciaRepositorio ocorrenciaRepositorio)
        {
            this.documentoRepositorio = documentoRepositorio;
            this.reclassificaDocumentoSeparadoServico = reclassificaDocumentoSeparadoServico;
            this.ocorrenciaRepositorio = ocorrenciaRepositorio;
            this.userSession = userSession;
        }
        
        public void Executar(int documentoId)
        {
            var documento = this.documentoRepositorio.ObterComPacote(documentoId);
            this.reclassificaDocumentoSeparadoServico.Executar(documento, TipoDocumento.CodigoAguardandoNovoTipo);

            var ocorrencia = new Ocorrencia
            {
                DataRegistro = DateTime.Now,
                Documento = documento,
                Lote = documento.Lote,
                Pacote = documento.Lote.Pacote,
                Status = OcorrenciaStatus.Registrada,
                UsuarioRegistro = (Usuario) this.userSession.UsuarioAtual,
                Tipo = new OcorrenciaTipo
                {
                    Id = OcorrenciaTipo.TipoDocumentoNaoEncontrado
                },
                Observacao = "Tipo de documento não cadastrado"
            };
            
            this.ocorrenciaRepositorio.Salvar(ocorrencia);
        }
    }
}
