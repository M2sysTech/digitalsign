namespace Veros.Paperless.Infra
{
    using System;
    using Framework.Modelo;
    using Framework.Servicos;
    using Model.Entidades;

    public class SessaoDoUsuarioServidoDoWindows : ISessaoDoUsuario
    {
        public IUsuario UsuarioAtual
        {
            get
            {
                return new Usuario { Id = Usuario.CodigoDoUsuarioSistema };
            }
        }

        public string Estacao
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public string Nome
        {
            get
            {
                return "Usuário de Serviço";
            }
        }

        public bool EstaAutenticado
        {
            get
            {
                return true;
            }
        }

        public void LogIn(IUsuario usuario, bool remember)
        {
            throw new System.NotImplementedException();
        }

        public void LogOut()
        {
            throw new System.NotImplementedException();
        }
    }
}
