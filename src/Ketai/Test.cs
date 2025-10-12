using RDE.Media.Audio;

namespace Ketai;

public sealed class Test{
  public async Task Play(){

    // AudioSource audio = new(){
    //   // SourcePath = "E:/carlo/Download/Pin/Music/YTM/If_I_could_(Original-Mix).ogg"
    //   // SourcePath = "E:/carlo/Download/Pin/Music/YTM/If_I_could__Original-Mix_.wav"
    // };

    AudioSource audio = new();
    await audio.AudioSourceAsync("E:/carlo/Download/Musica/ILLIT/ILLIT-MyWorld.mp3");


    AudioPlayer ap = new();
    await ap.LoadAsync(audio);
    Console.WriteLine(ap.source!.SourcePath);

    await ap.PlayAsync();
    Console.WriteLine(ap.DataLog.content);


  }


}
