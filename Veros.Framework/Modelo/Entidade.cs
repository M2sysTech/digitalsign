namespace Veros.Framework.Modelo
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Representa uma entidade
    /// </summary>
    [Serializable]
    public abstract class Entidade : IEntidade
    {
        public Entidade()
        {
        }

        public Entidade(int id)
        {
            this.Id = id;
        }

        public virtual int Id
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets a value indicating whether é novo
        /// </summary>
        [IgnoreDataMember]
        public virtual bool EhNovo
        {
            get { return this.Id.Equals(0); }
        }

        /// <summary>
        /// Compara duas entidades
        /// </summary>
        /// <param name="x">Entidade x</param>
        /// <param name="y">Entidade y</param>
        /// <returns>True se sao iguais</returns>
        public static bool operator ==(Entidade x, Entidade y)
        {
            return object.Equals(x, y);
        }

        /// <summary>
        /// Compara se duas entidades são diferentes
        /// </summary>
        /// <param name="x">Entidade x</param>
        /// <param name="y">Entidade y</param>
        /// <returns>True se são diferentes</returns>
        public static bool operator !=(Entidade x, Entidade y)
        {
            return (x == y) == false;
        }

        /// <summary>
        /// Calcula e retorna hashcode do objeto
        /// </summary>
        /// <returns>Hashcode do objeto</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Compara entidade com um objeto
        /// </summary>
        /// <param name="obj">objeto a ser comparado</param>
        /// <returns>true se for igual, false se for diferente</returns>
        public override bool Equals(object obj)
        {
            if (this.EhNovo)
            {
                return ReferenceEquals(this, obj);
            }

            var other = obj as Entidade;
            return other != null && other.Id.Equals(this.Id);
        }

        /// <summary>
        /// Retorna string concatenado com id da entidade. Se id for 0, retorna tambem o HashCode
        /// </summary>
        /// <returns>string concatenado com id da entidade</returns>
        public override string ToString()
        {
            var @return = this.GetType().Name + "#" + this.Id;

            if (this.EhNovo)
            {
                return @return + "@" + this.GetHashCode();
            }

            return @return;
        }
    }
}