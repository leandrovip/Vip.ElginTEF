using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vip.ElginTEF.Core;

public class JsonContractResolver : DefaultContractResolver
{
    #region Construtores

    public JsonContractResolver()
    {
        NamingStrategy = new CamelCaseNamingStrategy();
    }

    #endregion

    #region Métodos

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var prop = base.CreateProperty(member, memberSerialization);
        if (!prop.Writable)
        {
            var property = member as PropertyInfo;
            var hasPrivateSetter = property?.GetSetMethod(true) != null;
            prop.Writable = hasPrivateSetter;
        }

        return prop;
    }

    #endregion
}