using System.Text.Json.Serialization;
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
