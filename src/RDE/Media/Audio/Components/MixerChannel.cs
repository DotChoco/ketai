// using RDE.Core.Logs;
// using System.IO;
using System;


namespace RDE.Media.Audio;
public sealed class MixerChannel{

private uint _id = uint.MinValue;
private int _volume = default;
private string _name = string.Empty;

public AudioPlayer[] players = Array.Empty<AudioPlayer>();
public string Name {
  get => _name != null ? _name.ToString() : "MxC";
  set => _name = value;
}
public int Volume {
  get => _volume;
  set => _volume = value;
}

public MixerChannel() => _id = (uint)this.GetHashCode();




}
