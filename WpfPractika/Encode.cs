using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPractika
{
    public class Encode
    {
        public static string EncodeDecrypt(string str, ushort secretKey = 0x0088)
        {
            var ch = str.ToArray();
            string newStr = "";
            foreach (var c in ch)
                newStr += TopSecret(c, secretKey);
            return newStr;
        }
        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey);
            return character;
        }
    }
}
