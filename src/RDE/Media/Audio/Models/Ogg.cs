using NVorbis;
using NAudio.Wave;

namespace RDE.Media.Audio;
public sealed class Ogg: Format {

public Format LoadAudio(string SourcePath){
  _sourcePath = SourcePath;

  if(outputDevice == null){
    // Use NVorbis to read .ogg file
    var vorbisReader = new VorbisReader(SourcePath);

    // Convierte el archivo .ogg a un formato que NAudio pueda manejar
    _audioFile = new VorbisWaveReader(vorbisReader);

    // The audio will plays using NAudio in its own thread
    outputDevice = new WaveOutEvent();
    outputDevice.Init(_audioFile);
  }
  return this;
}

}
