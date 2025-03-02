using System.Text.Json;
using Microsoft.AspNetCore.Http;

public static class SessionExtensions
{
	private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
	{
		IncludeFields = true
	};

	public static void SetObject<T>(this ISession session, string key, T value)
	{
		session.SetString(key, JsonSerializer.Serialize(value, _jsonOptions));
	}

	public static T GetObject<T>(this ISession session, string key)
	{
		var value = session.GetString(key);
		return value == null ? default : JsonSerializer.Deserialize<T>(value, _jsonOptions);
	}
}