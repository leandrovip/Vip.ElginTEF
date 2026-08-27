using System.Globalization;
using Newtonsoft.Json;
using Vip.ElginTEF.Core;

namespace Vip.ElginTEF.Extensions;

internal static class JsonExtensions
{
    private static readonly JsonSerializerSettings _settings = new()
    {
        ContractResolver = new JsonContractResolver(),
        NullValueHandling = NullValueHandling.Ignore,
        Culture = CultureInfo.GetCultureInfo("pt-BR")
    };

    public static string Serialize(this object value)
    {
        return JsonConvert.SerializeObject(value, _settings);
    }

    public static T Deserialize<T>(this string value)
    {
        return JsonConvert.DeserializeObject<T>(value, _settings);
    }
}