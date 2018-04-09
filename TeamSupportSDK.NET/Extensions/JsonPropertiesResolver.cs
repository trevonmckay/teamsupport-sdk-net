using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TeamSupportSDK.NET.Attributes;

namespace TeamSupportSDK.NET.Extensions
{
    internal class JsonPropertiesResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            return objectType.GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(JsonIgnoreSerializationAttribute)))
                .ToList<MemberInfo>();
        }
    }
}
