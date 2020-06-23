using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClienteMercado.Utils.Utilitarios
{
    public static class ConverterPrimeirosCaracteresEmCaixaAlta
    {
        public static string ToProperCase(this string texto)
        {
            if ((texto != "") && (texto != null))
            {
                const string pattern = @"(?<=\w)(?=[A-Z])";
                string resultado = Regex.Replace(texto, pattern, " ", RegexOptions.None);

                return resultado.Substring(0, 1).ToUpper() + resultado.Substring(1);
            }

            return null;
        }
    }
}
