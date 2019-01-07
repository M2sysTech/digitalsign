namespace Veros.Paperless.Model.Entidades
{
    using System;
    using Framework.Modelo;

    public class ConsumoCarimboDigital : Entidade
    {
        public virtual Lote Lote
        {
            get;
            set;
        }

        public virtual DateTime AssinadoEm
        {
            get;
            set;
        }

        public virtual Documento Documento
        {
            get;
            set;
        }

        public static ConsumoCarimboDigital Criar(int documentoId, int loteId)
        {
            return new ConsumoCarimboDigital
            {
                Documento = new Documento { Id = documentoId },
                Lote = new Lote { Id = loteId },
                AssinadoEm = DateTime.Now
            };
        }
    }
}