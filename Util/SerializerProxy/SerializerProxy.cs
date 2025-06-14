using Newtonsoft.Json;
using NReJSON;
using StackExchange.Redis;

namespace InfrastructureToolKit.Util.SerializerProxy
{
    public class SerializerProxy : ISerializerProxy
    {
        public TResult Deserialize<TResult>(RedisResult serializedValue)
        {
            if (serializedValue.IsNull) return default;
            var str = (string)serializedValue;
            return JsonConvert.DeserializeObject<TResult>(str);
        }

        public string Serialize<TObjectType>(TObjectType obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
