using RDE.Media.Audio;

namespace ketai;
class Program {
  static void Main() {
    Console.Clear();

    AudioSource audio = new(){ SourcePath = "E:/carlo/Download/Pin/Music/YTM/ROMANCEPLANET - SEE YOUR FACE (S L O W E D).ogg" };

    AudioPlayer ap = new(){ source = audio };
    ap.Play();

    Console.WriteLine(ap.Log.content);


  }
}
