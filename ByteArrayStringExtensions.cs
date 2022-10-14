using System;

namespace Druware.Extensions
{
    public static class ByteArrayStringExtension
    {
        public static string ToString(this byte[] dBytes)
        {
            char[] rgbDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            string str = "";
            int i;
            for (i = 0; i < dBytes.Length; i++)
            {
                str += rgbDigits[dBytes[i] >> 4].ToString();
                str += rgbDigits[dBytes[i] & 0xf].ToString();
            }
            return str;
        }
    }
}

