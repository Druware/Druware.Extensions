namespace Druware.Extensions
{
    public static class StringByteArrayExtensions
    {
        // TODO: Add UnitTests for ByteArrayStringExtension

        public static byte[] ToByteArray(this string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }
    }
    
    
}

