using NAudio.Wave;
using NAudio.Flac;

namespace RDE.Media.Audio;
public sealed class Flac:Format{

public Format LoadAudio(string SourcePath){
  _sourcePath = SourcePath;
  if(outputDevice == null){
    _audioFile = new FlacReader(_sourcePath);
    outputDevice = new DirectSoundOut();
    outputDevice.Init(_audioFile);
  }
  return this;
}


}
