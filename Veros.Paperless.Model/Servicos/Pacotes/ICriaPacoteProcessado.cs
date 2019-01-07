namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using System;
    using Entidades;

    public interface ICriaPacoteProcessado
    {
        PacoteProcessado Executar(string nomeDoPacote, DateTime? horaUltimoArquivo);
    }
}