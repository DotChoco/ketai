using Ketai.Spf.Testing;
using Ketai.Spf.Auth;
using Ketai.Spf.Service;

namespace Ketai;
class Program{
  static async Task Main(){
    Console.Clear();

    // File Media Player
    // Test tst = new();
    // await tst.Play();



    // Spotify Player

    //Spotify Client
    Librespot.Run();

    //Aqui debe de ir el selector automatico de Dispositivos
    //TODO()
    // Console.ReadKey();


    await SpotifyAuth.LoadCredentials("./cde.json");

    SpfTest spfTest = new();
    await spfTest.Auth();
    // Console.ReadKey();
  }
}




