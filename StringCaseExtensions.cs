namespace Druware.Extensions
{
    /// <summary>
    /// A collection of extensions for handling adjusting the case style of a
    /// string.
    /// </summary>
	public static class StringCaseExtensions
    {
        /// <summary>
        /// Convert a string to Camel Case Style Notation
        /// </summary>
        /// <param name="s"></param>
        /// <returns>A string in the CamelCase Notation style</returns>
        public static string CamelCaseString(this string s)
        {
            string result = string.Empty;

            string[] parts = s.Split(' ');
            foreach (string part in parts)
                result += part.ToUpper()[..1] + part.ToLower()[1..];

            return result;
        }
    }
}

