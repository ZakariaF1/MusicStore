using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class HtmlEnumExtensions
    {
        public static HtmlString EnumToString<T>(this HtmlHelper helper)
        {
            var values = Enum.GetValues(typeof(T)).Cast<int>();
            var enumDictionary = values.ToDictionary(value => Enum.GetName(typeof(T), value));
            return new HtmlString(JsonConvert.SerializeObject(enumDictionary));
        }
    }
}