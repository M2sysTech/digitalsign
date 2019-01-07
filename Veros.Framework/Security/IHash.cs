namespace Veros.Framework.Security
{
    /// <summary>
    /// Contrato para implementações de Hash de texto e arquivo
    /// </summary>
    public interface IHash
    {
        string HashText(string value);

        string HashFile(string file);
    }
}
