using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Ketai.Spf.Player;
public class SpotifyPlayer{

  public async Task Play(string accessToken, string trackUri){
    using var client = new HttpClient();
      client.DefaultRequestHeaders.Authorization = new
      AuthenticationHeaderValue("Bearer", accessToken);

    var body = new { uris = new[] { $"spotify:track:{trackUri}" } };
    var jsonBody = JsonSerializer.Serialize(body);

    var request = new HttpRequestMessage(
        HttpMethod.Put, "https://api.spotify.com/v1/me/player/play"){
          Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
    };

    await client.SendAsync(request);
  }

}
