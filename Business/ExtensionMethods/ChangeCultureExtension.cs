using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ExtensionMethods
{
    public static class ChangeCultureExtension
    {
        public static string ChangeCharacters(this string word)
        {
            return word.ToLower().Replace('ı', 'i')
                       .Replace('ö', 'o')
                       .Replace('ü', 'u')
                       .Replace('ğ', 'g')
                       .Replace('ş', 's')
                       .Replace('ç', 'c');
        }
    }
}
