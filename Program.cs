using Ketai.Spf.Testing;
using Ketai.Spf.Auth;
using Ketai.Spf.Service;
using Ketai.Spf.Player;

namespace Ketai;
class Program{
  static async Task Main(){
    Console.Clear();

    // File Media Player
    // Test tst = new();
    // await tst.Play();




    // [----- Spotify Player -----]

    //Spotify LoadCredentials
    await SpotifyAuth.LoadCredentials("./cde.json");

    SpfTest spfTest = new();
    await spfTest.Auth();

    Console.WriteLine("Account Authenticated");

    //Spotify Client
    await Librespot.Run();
    Console.WriteLine("Client Connected");

    //Aqui debe de ir el selector automatico de Dispositivos
    await SpotifyDevices.ListDevices();

    SpotifyDevices spfDvcs = new();
    await spfDvcs.SelectDevice("Ketai2");
    Console.WriteLine("Device Linked");


    // Play Music
    await spfTest.Play();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n\nNow is playing: HypeBoy - NewJeans");
    Console.ForegroundColor = ConsoleColor.White;

    // Console.ReadKey();
  }
}




