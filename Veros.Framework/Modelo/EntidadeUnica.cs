namespace Veros.Framework.Modelo
{
    public abstract class EntidadeUnica : IEntidade
    {
        public virtual int Id
        {
            get;
            set;
        }

        public static bool operator ==(EntidadeUnica x, EntidadeUnica y)
        {
            return true;
        }

        public static bool operator !=(EntidadeUnica x, EntidadeUnica y)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return true;
        }
    }
}