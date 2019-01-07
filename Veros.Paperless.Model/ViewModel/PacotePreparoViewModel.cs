namespace Veros.Paperless.Model.ViewModel
{
    using Entidades;

    public class PacotePreparoViewModel
    {
        public int Id { get; set; }

        public string Identificacao { get; set; }

        public string DataInicioPreparoFormatado { get; set; }

        public string HoraInicioPreparoFormatado { get; set; }

        public string DataFimPreparoFormatado { get; set; }

        public string HoraFimPreparoFormatado { get; set; }

        public string Matricula1 { get; set; }

        public string Matricula2 { get; set; }

        public string Matricula3 { get; set; }

        public string Matricula4 { get; set; }

        public static PacotePreparoViewModel Criar(Pacote pacote)
        {
            return new PacotePreparoViewModel
            {
                Id = pacote.Id,
                Identificacao = pacote.Identificacao,
                DataInicioPreparoFormatado = pacote.DataInicioPreparo.HasValue ? pacote.DataInicioPreparo.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty,
                HoraInicioPreparoFormatado = pacote.DataInicioPreparo.HasValue ? pacote.DataInicioPreparo.GetValueOrDefault().ToString("HH:mm") : string.Empty,
                DataFimPreparoFormatado = pacote.DataFimPreparo.HasValue ? pacote.DataFimPreparo.GetValueOrDefault().ToString("dd/MM/yyyy") : string.Empty,
                HoraFimPreparoFormatado = pacote.DataFimPreparo.HasValue ? pacote.DataFimPreparo.GetValueOrDefault().ToString("HH:mm") : string.Empty,
                Matricula1 = pacote.UsuarioPreparo != null ? pacote.UsuarioPreparo.Login : string.Empty,
                Matricula2 = pacote.UsuarioPreparo2 != null ? pacote.UsuarioPreparo2.Login : string.Empty,
                Matricula3 = pacote.UsuarioPreparo3 != null ? pacote.UsuarioPreparo3.Login : string.Empty,
                Matricula4 = pacote.UsuarioPreparo4 != null ? pacote.UsuarioPreparo4.Login : string.Empty
            };
        }
    }
}
