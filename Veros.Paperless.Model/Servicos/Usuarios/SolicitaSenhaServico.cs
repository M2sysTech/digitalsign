namespace Veros.Paperless.Model.Servicos.Usuarios
{
    using System;
    using Framework.Modelo;
    using Repositorios;

    public class SolicitaSenhaServico : ISolicitaSenhaServico
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public SolicitaSenhaServico(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public string Executar(string login)
        {
            var usuario = this.usuarioRepositorio.ObterPeloLogin(login);

            if (usuario == null)
            {
                throw new RegraDeNegocioException("Funcional informado não localizado no cadastro do sistema.");
            }

            var senhaNova = this.CriarSenhaRandomica();

            this.usuarioRepositorio.AlterarSenha(usuario.Id, senhaNova, "S");

            return senhaNova;
        }

        private string CriarSenhaRandomica()
        {
            int tamanho = 6; // Numero de digitos da senha
            string senha = string.Empty;
            for (int i = 0; i < tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());
                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string charAtual = ((char)codigo).ToString();
                    if (!senha.Contains(charAtual))
                    {
                        senha += charAtual;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }

            return senha;
        }
    }
}
