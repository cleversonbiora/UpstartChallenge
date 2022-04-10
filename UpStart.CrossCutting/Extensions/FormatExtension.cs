using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UpStart.CrossCutting.Extensions
{
    public static class FormatExtension
    {
        public static string GetNumbers (this string input)
        {
            return Regex.Replace(input, @"[^\d]", "");
        }
    }
}
