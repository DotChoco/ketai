using Ketai.Spf.Testing;

namespace Ketai;
class Program{
  static async Task Main(){
    Console.Clear();

    // File Media Player
    // Test tst = new();
    // await tst.Play();



    // [----- Spotify Player -----]

    //Spotify Account Auth
    SpfTest spfTest = new();
    await spfTest.Auth();

    //Run Librespot and Connection with it
    await spfTest.DeviceConnect();

    // Play Music
    await spfTest.Play();

    Console.ReadKey();
  }
}




