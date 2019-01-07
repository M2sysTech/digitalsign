namespace Veros.Framework.Threads
{
    using System.Threading.Tasks;

    public static class Paralelizar
    {
        public static ParallelOptions Em(int nucleos)
        {
            return new ParallelOptions
            {
                MaxDegreeOfParallelism = nucleos
            };
        }
    }
}
