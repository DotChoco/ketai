using System.Net.Http.Headers;
using Ketai.Spf.Auth;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text;

namespace Ketai.Spf.Player;


sealed class Payload{
  public string[]? device_ids { get; set; }
  public bool play { get; set; }
}

public sealed class LDevices{
  [JsonPropertyName("devices")]
  public List<Device> devices { get; set; } = new();
}


public class SpotifyDevices{
 private LDevices _deviceList = new();
 public LDevices DeviceList { get => _deviceList; set => value = _deviceList; }

  public SpotifyDevices(){}


  public async Task ListDevices(){
    HttpClient client = new();
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", ClientTokens.AccessToken);

    var response = await client.GetAsync("https://api.spotify.com/v1/me/player/devices");

    if (!response.IsSuccessStatusCode){
        Console.WriteLine($"Error: {response.StatusCode}");
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        return;
    }

    string json = await response.Content.ReadAsStringAsync();
    var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };

    _deviceList = JsonSerializer.Deserialize<LDevices>(json, options)!;
  }



  public async Task SelectDevice(string deviceName = ""){
    if(deviceName == null || deviceName == string.Empty)
      deviceName = Service.Librespot.deviceName;

    if(_deviceList == null || _deviceList.devices == null || _deviceList.devices.Count == 0)
      await ListDevices();
    Device? _dvc = _deviceList.devices.Find(x => x.name == deviceName);

    HttpClient client = new();
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
