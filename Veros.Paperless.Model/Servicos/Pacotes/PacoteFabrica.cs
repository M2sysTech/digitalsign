namespace Veros.Paperless.Model.Servicos.Pacotes
{
    using System;
    using Entidades;

    public class PacoteFabrica
    {
        public Pacote Criar(string identificacao, int coletaId, string status = "R")
        {
            var caixa = new Pacote
            {
                UsuarioQueCapturouId = 0,
                HoraInicio = DateTime.Now,
                Estacao = "1234",
                Status = status,
                Batido = "S",
                Agencia = 1234,
                Bdu = "1234",
                DataMovimento = DateTime.Now,
                Identificacao = identificacao,
                Coleta = new Coleta { Id = coletaId }
            };

            return caixa;
        }        
    }
}
