using Microsoft.AspNetCore.Html;


namespace MusicStoreWebApp.ExtensionMethods
{
    public static class HtmlContentExtensions
    {
        public static string ToHtmlString(this IHtmlContent htmlContent)
        {
            using (var writer = new System.IO.StringWriter())
            {
                htmlContent.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}