using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;

namespace Ketai.Spf.Auth;

public static class ClientCredentials {
  public static string ClientId { get; set; } = string.Empty;
  public static string ClientSecret { get; set; } = string.Empty;
  public static string RedirectUri { get; set; } = string.Empty;
}

public static class ClientTokens{
  public static string AccessToken { get; set; } = string.Empty;
  public static string RefreshToken { get; set; } = string.Empty;
}

public class SpotifyTokenResponse{
  [JsonPropertyName("access_token")]
  public string AccessToken { get; set; } = "";

  [JsonPropertyName("token_type")]
  public string TokenType { get; set; } = "";

  [JsonPropertyName("expires_in")]
  public int ExpiresIn { get; set; }

  [JsonPropertyName("refresh_token")]
  public string RefreshToken { get; set; } = "";
}

sealed class Credentials {
  public string ClientId { get; set; } = string.Empty;
  public string ClientSecret { get; set; } = string.Empty;
  public string RedirectUri { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
}




public class SpotifyAuth{
  private string redirectUri = string.Empty;
  private static Credentials S_CDE = new();


  public async Task<string> GetCodeFromSpotify(){
    string scopes = "user-modify-playback-state user-read-playback-state user-read-currently-playing";
    redirectUri = (string)ClientCredentials.RedirectUri.Clone();

    string authUrl = $"https://accounts.spotify.com/authorize?response_type=code&client_id={ClientCredentials.ClientId}&scope={Uri.EscapeDataString(scopes)}&redirect_uri={Uri.EscapeDataString(redirectUri.Remove(redirectUri.Length-1))}";

    Console.WriteLine("Abre en el navegador: " + authUrl);

    var listener = new HttpListener();
    listener.Prefixes.Add(this.redirectUri);
    listener.Start();

    Console.WriteLine("Esperando redirección de Spotify...");

    var context = await listener.GetContextAsync();
    string code = context.Request.QueryString["code"]!;

    listener.Stop();
    return code!;
  }


  public async Task GetTokens(string code){
    using var client = new HttpClient();
    string redirectUri = this.redirectUri.Remove(this.redirectUri.Length-1);

    var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token"){
      Content = new FormUrlEncodedContent(new Dictionary<string, string>{
        { "grant_type", "authorization_code" },
        { "code", code },
        { "redirect_uri", redirectUri },
        { "client_id", ClientCredentials.ClientId },
        { "client_secret", ClientCredentials.ClientSecret }
      })
    };


    var response = await client.SendAsync(request);
    var json = await response.Content.ReadAsStringAsync();


    if (!response.IsSuccessStatusCode) {
      Console.WriteLine("Error getting tokens: \n" + json);
      return;
    }


    var tokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(json);

    // Console.WriteLine(tokenResponse!.RefreshToken);

    ClientTokens.AccessToken = tokenResponse!.AccessToken;
    ClientTokens.RefreshToken = tokenResponse.RefreshToken;
  }


  public static async Task LoadCredentials(string path){
    StreamReader sr =new(path);
    string json = await sr.ReadToEndAsync();
    sr.Close();

    S_CDE = JsonSerializer.Deserialize<Credentials>(json)!;

    ClientCredentials.ClientId = S_CDE!.ClientId;
    ClientCredentials.ClientSecret = S_CDE!.ClientSecret;
    ClientCredentials.RedirectUri = S_CDE!.RedirectUri;
    ClientTokens.RefreshToken = S_CDE!.RefreshToken;
  }

  public static async Task SaveTokens(string path){
    string json = JsonSerializer.Serialize(S_CDE, new JsonSerializerOptions {
      Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    });

    StreamWriter sw = new(path);
    await sw.WriteLineAsync(json);
    sw.Close();

  }



  public async Task RefreshAccessToken()
  {
    using var client = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

    // Authorization: Basic base64(client_id:client_secret)
    var basicAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{S_CDE.ClientId}:{S_CDE.ClientSecret}"));
    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuth);

    var content = new FormUrlEncodedContent(new[]
    {
        new KeyValuePair<string,string>("grant_type", "refresh_token"),
        new KeyValuePair<string,string>("refresh_token", S_CDE.RefreshToken)
    });
    request.Content = content;

    var resp = await client.SendAsync(request);
    var body = await resp.Content.ReadAsStringAsync();

    if (!resp.IsSuccessStatusCode)
    {
        Console.WriteLine("Error refreshing token: " + resp.StatusCode);
        Console.WriteLine(body);
        return;
    }

    using var doc = JsonDocument.Parse(body);
    var root = doc.RootElement;
    var accessToken = root.GetProperty("access_token").GetString();
    var expiresIn = root.GetProperty("expires_in").GetInt32();

    // Nota: a veces la respuesta incluye refresh_token; si la incluye, reemplazar tu guardado.
    if (root.TryGetProperty("refresh_token", out var newRefresh)){
        ClientTokens.RefreshToken = newRefresh.GetString()!;
    }

    ClientTokens.AccessToken = accessToken!;
    await SaveTokens("../../cde.json");
}

}



