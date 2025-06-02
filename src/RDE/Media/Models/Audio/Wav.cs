using NAudio.Wave;
namespace RDE.Media.Audio;

public sealed class Wav:GuideLines {
public Wav(){}


public void Play(string SourcePath){
  _sourcePath = SourcePath;
  var audioFile = new AudioFileReader(_sourcePath);
  var outputDevice = new WaveOutEvent();
  outputDevice.Init(audioFile);
  outputDevice.Play();

  Console.WriteLine("Reproduciendo sonido WAV... Presiona 'CTRL + C' para salir.");
  Thread.Sleep(audioFile.TotalTime);
  // Console.WriteLine(audioFile.TotalTime);
}


}
