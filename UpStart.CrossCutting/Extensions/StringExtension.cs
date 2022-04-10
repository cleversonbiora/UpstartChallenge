using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace UpStart.CrossCutting.Extensions
{
    public static class StringExtension
    {
        public static T Transform<T>(this string value)
        {
            if (typeof(T) == typeof(byte[]))
                return (T)(object)Encoding.ASCII.GetBytes(value);
            else if (typeof(T) == typeof(string))
                return (T)(object)value;
            else if (typeof(T) == typeof(int))
                return (T)(object)Int32.Parse(value);
            else if (typeof(T) == typeof(decimal))
                return (T)(object)Decimal.Parse(value);
            else if (typeof(T) == typeof(bool))
                return (T)(object)Boolean.Parse(value);
            else if (typeof(T) == typeof(char))
                return (T)(object)Char.Parse(value);
            return JsonSerializer.Deserialize<T>(value);
        }

        public static string MaskCep(this string cep)
        {
            try
            {
                return Convert.ToUInt64(cep).ToString(@"00000\-000");
            }
            catch
            {
                return cep;
            }
        }

        public static decimal ToDecimal(this string content)
        {
            return Convert.ToDecimal(content, new CultureInfo("en-US"));
        }

        public static long ToLong(this string content)
        {
            return Convert.ToInt64(content);
        }

        public static string JustNumber(this string text)
        {
            return new String(text.Where(Char.IsDigit).ToArray());
        }

    }
}
