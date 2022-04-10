using System;

namespace UpStart.CrossCutting.Extensions
{
    public static class DateTimeExtentions
    {
        public static string ToReadWithTimeFormat(this DateTime value)
        {
            return value.ToString("dd/MMM/yyyy HH:mm");
        }
    }
}
