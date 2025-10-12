using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

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





public class SpotifyAuth{
  private string redirectUri = string.Empty;


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


    string responseString = "<html><body><h1>Ya puedes cerrar esta ventana.</h1></body></html>";
    var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
    context.Response.ContentLength64 = buffer.Length;

    await context.Response.OutputStream.WriteAsync(buffer);
    context.Response.OutputStream.Close();

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
      Console.WriteLine("Error obteniendo tokens: \n" + json);
      return;
    }


    var tokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(json);

    ClientTokens.AccessToken = tokenResponse!.AccessToken;
    ClientTokens.RefreshToken = tokenResponse.RefreshToken;

  }


  public async Task SaveTokens(string path, string tokens){
    string json = JsonSerializer.Serialize(tokens, new JsonSerializerOptions {
      Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    });

    StreamWriter sw = new(path);
    await sw.WriteLineAsync(json);
    sw.Close();

    Console.WriteLine("Tokens Created");
  }

  public async Task LoadTokens(string path){
    StreamReader sr =new(path);
    string data = await sr.ReadToEndAsync();
    sr.Close();

    data = JsonSerializer.Deserialize<string>(data)!;

    Console.WriteLine("Tokens Loaded");
    SpotifyTokenResponse response = JsonSerializer.Deserialize<SpotifyTokenResponse>(data)!;

    ClientTokens.AccessToken = response.AccessToken;
    ClientTokens.RefreshToken = response.RefreshToken;
  }

}



