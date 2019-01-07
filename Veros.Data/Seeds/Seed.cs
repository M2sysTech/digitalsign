namespace Veros.Data.Seeds
{
    using Veros.Data.Hibernate;

    public abstract class Seed : DaoBase
    {
        public string Nome
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public abstract void Executar();

        public abstract string VersaoDoSeed();

        public bool EstaAtualizado(string versaoParaComparar)
        {
            return this.VersaoDoSeed().Equals(versaoParaComparar);
        }
    }
}
