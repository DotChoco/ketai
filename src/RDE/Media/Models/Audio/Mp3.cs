using NAudio.Wave;

namespace RDE.Media.Audio;

public sealed class Mp3: GuideLines{
public Mp3(){}

public void Play(string SourcePath){
  _sourcePath = SourcePath;
  var audioFile = new Mp3FileReader(SourcePath);
  var outputDevice = new WaveOutEvent();
  outputDevice.Init(audioFile);
  outputDevice.Play();

  Console.WriteLine("Reproduciendo MP3... Presiona Enter para detener.");
  Thread.Sleep(audioFile.TotalTime);
  // Console.WriteLine(audioFile.TotalTime);
}


}
