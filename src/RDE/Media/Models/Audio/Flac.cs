using NAudio.Wave;
namespace RDE.Media.Audio;

public sealed class Flac: GuideLines{

public Flac(){}

public void Play(string SourcePath){
  _sourcePath = SourcePath;
  using var reader = new NAudio.Flac.FlacReader(_sourcePath);
  using var player = new DirectSoundOut();
  player.Init(reader);
  player.Play();

  /*Console.WriteLine("Now playing with NAudio.Flac back-end, press any key to continue.");*/
  /*Thread.Sleep(reader.TotalTime);*/
  Console.WriteLine(reader.TotalTime);

}



}
