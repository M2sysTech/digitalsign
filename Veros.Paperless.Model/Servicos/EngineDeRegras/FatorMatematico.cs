namespace Veros.Paperless.Model.Servicos.EngineDeRegras
{
    using System;
    using Entidades;

    /// <summary>
    /// TODO: refatorar
    /// </summary>
    public class FatorMatematico
    {
        public static string Aplicar(string valor, OperadorMatematico operador, string fator, TipoDado tipoDado)
        {
            if (tipoDado == TipoDado.DateTime)
            {
                if (string.IsNullOrEmpty(valor) == false && valor.Length == 8)
                {
                    valor = string.Format("{0}/{1}/{2}",
                        valor.Substring(0, 2),
                        valor.Substring(2, 2),
                        valor.Substring(4));
                }
            }

            if (operador == OperadorMatematico.Adicao)
            {
                double @double;
                if (double.TryParse(valor, out @double))
                {
                    valor = Convert.ToString(@double + Convert.ToDouble(fator));
                }  
                else
                {
                    DateTime dateTime;
                    if (DateTime.TryParse(valor,
                        out dateTime))
                    {
                        valor = dateTime.AddDays(Convert.ToDouble(fator)).ToString("dd/MM/yyyy");
                    }
                }
            }

            if (operador == OperadorMatematico.Subtracao)
            {
                double @double;
                if (double.TryParse(valor, out @double))
                {
                    valor = Convert.ToString(@double - Convert.ToDouble(fator));
                }
                else
                {
                    DateTime dateTime;
                    if (DateTime.TryParse(valor, out dateTime))
                    {
                        valor = dateTime
                            .AddDays(-1 * Convert.ToDouble(fator))
                            .ToString("dd/MM/yyyy");
                    }
                }
            }

            if (operador == OperadorMatematico.Multiplicacao)
            {
                double @double;
                if (double.TryParse(valor, out @double))
                {
                    valor = Convert.ToString(@double * Convert.ToDouble(fator.Replace('.', ',')));
                }
                else
                {
                    valor = "0";
                }
            }

            return valor;
        }
    }
}
