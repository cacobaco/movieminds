using Newtonsoft.Json.Linq;

namespace Movieminds.Application.Contracts;

public interface IJsonMapper<T>
{
    T Map(JObject json);
    IEnumerable<T> Map(JArray jsonArray);
}
