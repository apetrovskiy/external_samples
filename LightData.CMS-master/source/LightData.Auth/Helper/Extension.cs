using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace LightData.Auth.Helper
{
    public static class Extension
    {
        public static ExternalActionResult ToJsonResult(this object item)
        {
            return new ExternalActionResult(item);
        }

        public static async Task<ExternalActionResult> ToJsonResultAsync(this object item)
        {
            return await Task.FromResult<ExternalActionResult>(new ExternalActionResult(item));
        }

        public static void SessionSet<T>(this T item, string key)
        {
            HttpContext.Current.Session[key] = item;
        }

        public static object SessionGet(this string key)
        {
            return HttpContext.Current.Session[key];
        }
        public static T SessionGet<T>(this string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        public static string ToJson(this object data)
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(data, Formatting.Indented, camelCaseFormatter);
        }
    }
}