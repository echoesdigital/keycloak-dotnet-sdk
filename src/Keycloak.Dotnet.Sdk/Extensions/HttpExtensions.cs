namespace Echoes.Digital.Keycloak.Dotnet.Sdk.Extensions;

public static class HttpExtensions
{
    public static string ToQueryString<TFilter>(this TFilter? filter)
        where TFilter : FilterBaseDto
    {
        if (filter == default) return string.Empty;

        var serializedObject = JsonSerializer.Serialize(filter);
        var deserializedObject = JsonSerializer.Deserialize<IDictionary<string, object?>>(serializedObject);
        var parameterList = deserializedObject?.Where(w => !string.IsNullOrWhiteSpace(w.Value?.ToString()) && !w.Key.Equals("q"))
            .Select(x => HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value?.ToString()))
            .ToList();

        var parameters = parameterList != null && parameterList.Any() ? $"{string.Join("&", parameterList)}" : "";
        var attributes = string.Empty;

        if (filter.Attributes != null && filter.Attributes.Any())
        {
            attributes = string.Join("&", filter.Attributes.Select(s => $"q={s.Key}:{s.Value}"));
        }

        var finalParameters = new[] { parameters, attributes };

        return finalParameters.Any() ? $"?{string.Join("&", finalParameters.Where(s => !string.IsNullOrWhiteSpace(s)))}" : "";
    }

    public static async Task<T?> ReadAsJsonAsync<T>(this HttpContent content)
    {
        var dataAsString = await content.ReadAsStringAsync().ConfigureAwait(false);
        var options = new JsonSerializerOptions();
        options.Converters.Add(new Converters_DateTimeConverter());

        return JsonSerializer.Deserialize<T>(dataAsString, options);
    }

    public static Task<HttpResponseMessage> PostJsonAsync<TData>(this HttpClient httpClient, Uri url, TData data)
    {
        var jsonRequest = JsonSerializer.Serialize(data, options: SerializerExtensions.SerializeOptions);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        return httpClient.PostAsync(url, content);
    }

    public static Task<HttpResponseMessage> DeleteJsonAsync<TData>(this HttpClient httpClient, Uri url, TData data)
    {
        var jsonRequest = JsonSerializer.Serialize(data, options: SerializerExtensions.SerializeOptions);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var message = new HttpRequestMessage(HttpMethod.Delete, url);
        message.Content = content;

        return httpClient.SendAsync(message);
    }

    public static Task<HttpResponseMessage> PutJsonAsync<TData>(this HttpClient httpClient, Uri url, TData data)
    {
        var jsonRequest = JsonSerializer.Serialize(data, options: SerializerExtensions.SerializeOptions);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var message = new HttpRequestMessage(HttpMethod.Put, url);
        message.Content = content;

        return httpClient.SendAsync(message);
    }
}
