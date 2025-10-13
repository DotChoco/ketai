using System.Net.Http.Headers;
using Ketai.Spf.Auth;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace Ketai.Spf.Player;

public sealed class Device{
  [JsonPropertyName("id")]
  public string id { get; set; } = string.Empty;

  [JsonPropertyName("is_active")]
  public bool is_active { get; set; } = false;

  [JsonPropertyName("is_private_session")]
  public bool is_private_session { get; set; } = false;

  [JsonPropertyName("is_restricted")]
  public bool is_restricted { get; set; } = false;

  [JsonPropertyName("name")]
  public string name { get; set; } = string.Empty;

  [JsonPropertyName("supports_volume")]
  public bool supports_volume { get; set; } = false;

  [JsonPropertyName("type")]
  public string type { get; set; } = string.Empty;

  [JsonPropertyName("volume_percent")]
  public byte volume_percent { get; set; } = byte.MinValue;
}

public sealed class LDevices{
  [JsonPropertyName("devices")]
  public List<Device> devices { get; set; } = new();
}

public class Payload{
  public string[]? device_ids { get; set; }
  public bool play { get; set; }
}

public class SpotifyDevices{
  public static LDevices DeviceList = new();



  public static async Task ListDevices(){
    var client = new HttpClient();
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", ClientTokens.AccessToken);

    var response = await client.GetAsync("https://api.spotify.com/v1/me/player/devices");

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"Error: {response.StatusCode}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return;
    }

    var json = await response.Content.ReadAsStringAsync();
    var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };

    DeviceList = JsonSerializer.Deserialize<LDevices>(json, options)!;
  }



  public async Task SelectDevice(string deviceName){
    Device? _dvc = DeviceList.devices.Find(x => x.name == deviceName);

    var client = new HttpClient();
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ClientTokens.AccessToken}");

    Payload payload = new();
    payload.device_ids = new string[] { _dvc.id };
    payload.play = false;

    var content = new StringContent(
      JsonSerializer.Serialize(payload),
      Encoding.UTF8,
      "application/json"
    );

    var response = await client.PutAsync("https://api.spotify.com/v1/me/player", content);
    // if (response.IsSuccessStatusCode)
    // {
    //     Console.WriteLine($"✅ Dispositivo '{deviceName}' seleccionado");
    // }
    // else
    // {
    //     Console.WriteLine($"❌ Error: {response.StatusCode}");
    //     Console.WriteLine(await response.Content.ReadAsStringAsync());
    // }
 }




}
