using System.Globalization;
using Newtonsoft.Json;
using Vip.ElginTEF.Core;

namespace Vip.ElginTEF.Extensions;

internal static class JsonExtensions
{
    public static string Serialize(this object value)
    {
        return JsonConvert.SerializeObject(value, ObterSettings());
    }

    public static T Deserialize<T>(this string value)
    {
        return JsonConvert.DeserializeObject<T>(value, ObterSettings());
    }

    private static JsonSerializerSettings ObterSettings()
    {
        var defaultContract = new JsonContractResolver();
        return new JsonSerializerSettings
        {
            ContractResolver = defaultContract,
            NullValueHandling = NullValueHandling.Ignore,
            Culture = CultureInfo.GetCultureInfo("pt-BR")
        };
    }
}