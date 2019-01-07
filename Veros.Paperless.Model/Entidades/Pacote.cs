namespace Veros.Paperless.Model.Entidades
{
    using System;
    using System.Collections.Generic;
    using Framework.Modelo;
    using Iesi.Collections.Generic;

    [Serializable]
    public class Pacote : Entidade
    {
        public const string ErroAoExportar = "W";
        public const string ParaExportar = "E";
        public const string Fechado = "F";
        public const string Aberto = "A";
        public const string Recebido = "R";
        public const string Importando = "I";
        public const string NaoRecebidoNaRecepcao = "N";
        public const string FimConferencia = "C";
        public const string Devolvida = "D";
        public const string MarcadaParaDevolucao = "X";
        public const string MarcadaComoDuplicada = "L";

        public Pacote()
        {
            this.Lotes = new List<Lote>();
            this.DossiesEsperados = new List<DossieEsperado>();
        }

        public virtual int UsuarioQueCapturouId
        {
            get;
            set;
        }

        public virtual DateTime HoraInicio
        {
            get;
            set;
        }

        public virtual DateTime HoraFim
        {
            get;
            set;
        }

        public virtual string Estacao
        {
            get;
            set;
        }

        public virtual string Status
        {
            get;
            set;
        }

        public virtual string Batido
        {
            get;
            set;
        }

        public virtual string ToHost
        {
            get;
            set;
        }

        public virtual string FromHost
        {
            get;
            set;
        }

        public virtual int TotEnv
        {
            get;
            set;
        }

        public virtual int Agencia
        {
            get;
            set;
        }

        public virtual string Bdu
        {
            get;
            set;
        }

        public virtual DateTime DataMovimento
        {
            get;
            set;
        }

        public virtual string Identificacao
        {
            get;
            set;
        }

        public virtual string CodigoAlternativo
        {
            get;
            set;
        }

        public virtual Coleta Coleta
        {
            get;
            set;
        }

        public virtual DateTime DataConferencia
        {
            get;
            set;
        }

        public virtual Usuario UsuarioConferencia
        {
            get;
            set;
        }

        public virtual DateTime? DataInicioPreparo
        {
            get;
            set;
        }

        public virtual DateTime? DataFimPreparo
        {
            get;
            set;
        }

        public virtual Usuario UsuarioPreparo
        {
            get;
            set;
        }

        public virtual Usuario UsuarioPreparo2
        {
            get;
            set;
        }

        public virtual Usuario UsuarioPreparo3
        {
            get;
            set;
        }

        public virtual Usuario UsuarioPreparo4
        {
            get;
            set;
        }

        public virtual List<Lote> Lotes
        {
            get;
            set;
        }

        public virtual List<DossieEsperado> DossiesEsperados
        {
            get;
            set;
        }

        public virtual List<Ocorrencia> Ocorrencias
        {
            get;
            set;
        }

        public virtual int ColetaId
        {
            get
            {
                return this.Coleta != null ? this.Coleta.Id : 0;
            }

            set
            {
                this.Coleta = new Coleta { Id = value };
            }
        }

        public virtual bool EtiquetaCefGerada
        {
            get;
            set;
        }

        public virtual DateTime? DataDevolucao
        {
            get;
            set;
        }
    }
}
