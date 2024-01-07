using System.Web;

namespace Druware.Extensions
{
    /// <summary>
    /// Provide a set of quick handlers for processing Html in strings
    /// </summary>
    public static class StringHtmlExtensions
    {
        // TODO: Document each of the functions and create unit tests for them


        public static string DecodeHtml(this string input) =>
            HttpUtility.HtmlDecode(input);

        public static string EncodeHtml(this string input) =>
            HttpUtility.HtmlEncode(input);

        public static string DecodeUrl(this string input) =>
            HttpUtility.UrlDecode(input);

        public static string EncodeUrl(this string input) =>
            HttpUtility.UrlEncode(input);

        public static string NormalizeHtml(this string inputData)
        {
            string result = inputData;

            // remove formatting html
            result = result.Replace("\n", "");
            result = result.Replace("\t", "");

            // line breaks
            result = result.Replace("<br>", "\n");
            result = result.Replace("<br/>", "\n");
            result = result.Replace("<br />", "\n");
            result = result.Replace("<BR>", "\n");
            result = result.Replace("<BR/>", "\n");
            result = result.Replace("<BR />", "\n");

            // bold tags
            result = result.Replace("<b>", "");
            result = result.Replace("</b>", "");
            result = result.Replace("<B>", "");
            result = result.Replace("</B>", "");

            // italics
            result = result.Replace("<i>", "");
            result = result.Replace("</i>", "");
            result = result.Replace("<I>", "");
            result = result.Replace("</I>", "");

            // deal with complex tags
            if (result.Contains("<font "))
                result = result[..result.IndexOf("<font", StringComparison.CurrentCulture)] +
                    result[(1 + result.IndexOf(">", result.IndexOf("<font", StringComparison.CurrentCulture), StringComparison.CurrentCulture))..];
            result = result.Replace("</font>", "");
            if (result.Contains("<FONT "))
                result = result[..result.IndexOf("<FONT", StringComparison.CurrentCulture)] +
                    result[(1 + result.IndexOf(">", result.IndexOf("<FONT", StringComparison.CurrentCulture), StringComparison.CurrentCulture))..];
            result = result.Replace("</FONT>", "");

            // replace various Html Entities

            result = result.Replace("&nbsp;", " ");
            result = result.Replace("&amp;", "&");

            result = result.Replace("&035;", "#");
            result = result.Replace("&037;", "%");

            return result;
        }
    }
}

