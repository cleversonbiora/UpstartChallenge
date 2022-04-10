namespace UpStart.CrossCutting.Extensions
{
    public static class DecimalExtention
    {
        public static string ToReadFormat(this decimal value)
        {
            return string.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), "{0:N}", value);
        }
    }
}
