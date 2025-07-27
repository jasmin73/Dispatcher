using System.Net;
using System.Text.RegularExpressions;

namespace Dispatcher.Server.Helper
{
    public static class HtmlHelper
    {
        public static string StripHtml(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var noHtml = Regex.Replace(input, "<.*?>", string.Empty);
            return WebUtility.HtmlDecode(noHtml).Replace("\u00A0", " ").Trim();
        }
    }
}
