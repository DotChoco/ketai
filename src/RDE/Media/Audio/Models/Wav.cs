using NAudio.Wave;

namespace RDE.Media.Audio;
public sealed class Wav:Format{

public Format LoadAudio(string SourcePath){
  _sourcePath = SourcePath;

  if(outputDevice == null){
    _audioFile = new AudioFileReader(_sourcePath);
    outputDevice = new WaveOutEvent();
    outputDevice.Init(_audioFile);
  }
  return this;
}


}
