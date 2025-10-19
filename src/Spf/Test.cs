using Ketai.Spf.Auth;
using Ketai.Spf.Player;
using Ketai.Spf.Service;

namespace Ketai.Spf.Testing;

public sealed class SpfTest{

  public async Task Auth(){
    SpotifyAuth auth = new();
    auth.CredentialPath = "D:/Github/CSharp/ketai/cache/cde.json";

    // Create new AccessToken
    // await auth.CreateAccessToken();


    // Use and Update AccessToken
    await auth.LoadCredentials();
    await auth.RefreshAccessToken();

    await auth.SaveTokens();
    Console.WriteLine("Account Authenticated");
  }


  public async Task DeviceConnect(){
    //Spotify Client
    Librespot.CachePath = "D:/cache/";
    Librespot.deviceName = "Ketai Aquos";
    // Librespot.Authorize("Ketai Aquos");
    // Librespot.Restart();
    Librespot.Run();
    Console.WriteLine("Client Connected");



    //Device Connection
    // await SpotifyDevices.ListDevices();

    SpotifyDevices spfDvcs = new();
    await spfDvcs.SelectDevice();
    Console.WriteLine("Device Linked");
  }

  public async Task Play(){
    SpotifyPlayer spfPlayer = new();

    // Play a song (Only Premium + Active Device)
    // 56v8WEnGzLByGsDAXDiv4d   # ETA - NewJeans
    // 5sdQOyqq2IDhvmx2lHOpwd   # SuperShy - NewJeans
    // 0a4MMyCrzT0En247IhqZbD   # HypeBoy - NewJeans
    // 65FftemJ1DbbZ45DUfHJXE   # OMG - NewJeans


    string songId = "0a4MMyCrzT0En247IhqZbD";
    await spfPlayer.Play(ClientTokens.AccessToken, songId);

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\nNow is playing: HypeBoy - NewJeans");
    Console.ForegroundColor = ConsoleColor.White;

  }


}
