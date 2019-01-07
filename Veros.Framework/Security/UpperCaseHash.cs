namespace Veros.Framework.Security
{
    public class UpperCaseHash : HashBase
    {
        public override string Format
        {
            get
            {
                return "X2";
            }
        }
    }
}
