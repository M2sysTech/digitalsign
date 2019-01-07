namespace Veros.Paperless.Model.Servicos
{
    using System;
    using System.Threading.Tasks;
    using Data;
    using Framework;
    using Veros.Paperless.Model.Entidades;
    using Veros.Paperless.Model.Repositorios;

    public class GravaLogGenericoServico : IGravaLogGenericoServico
    {
        private readonly ILogGenericoRepositorio logGenericoRepositorio;
        private readonly IUnitOfWork unitOfWork;
        
        public GravaLogGenericoServico(
            ILogGenericoRepositorio logGenericoRepositorio,
            IUnitOfWork unitOfWork)
        {
            this.logGenericoRepositorio = logGenericoRepositorio;
            this.unitOfWork = unitOfWork;
        }

        public void Executar(string acao, int registro, string observacao, string modulo, string usuarioMatricula = "SYS")
        {
            new TaskFactory().StartNew(() =>
            {
                try
                {
                    var logGenerico = new LogGenerico
                    {
                        Acao = acao,
                        Registro = registro,
                        Observacao = observacao,
                        Modulo = modulo,
                        UsuarioNome = usuarioMatricula
                    };

                    this.unitOfWork.Transacionar(() => this.logGenericoRepositorio.Salvar(logGenerico));
                }
                catch (Exception exception)
                {
                    Log.Application.Error(exception);
                }
            });
        }
    }
}