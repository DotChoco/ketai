using NAudio.Wave;

namespace RDE.Media.Audio;
public sealed class Mp3:Format{

public Format LoadAudio(string SourcePath){
  _sourcePath = SourcePath;

  if(outputDevice == null){
    _audioFile = new Mp3FileReader(SourcePath);
    outputDevice = new WaveOutEvent();
    outputDevice.Init(_audioFile);
  }
  return this;
}



}
