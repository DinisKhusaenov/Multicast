using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Infrastructure.Serialization
{
  public static class JsonSerialization
  {
    public static string ToJson(this object self) =>
      JsonConvert.SerializeObject(self, new JsonSerializerSettings
      {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
      });

    public static T FromJson<T>(this string json) =>
      JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
      {
        TypeNameHandling = TypeNameHandling.Auto,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
      });
  }}