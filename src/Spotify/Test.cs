using Ketai.Spf.Auth;
using Ketai.Spf.Player;

namespace Ketai.Spf.Testing;

public sealed class SpfTest{

  public async Task Auth(){
    var auth = new SpotifyAuth();
    var spfPlayer = new SpotifyPlayer();

    string code = await auth.GetCodeFromSpotify();
    await auth.GetTokens(code);

    // Play a song (Only Premium + Active Device)
    // 6sdQOyqq2IDhvmx2lHOpwd   # SuperShy - NewJeans
    // 0a4MMyCrzT0En247IhqZbD   # HypeBoy - NewJeans
    string songId = "0a4MMyCrzT0En247IhqZbD";
    await spfPlayer.Play(ClientTokens.AccessToken, songId);
  }

  public async Task LoadTokens(string jsonPath){
    var auth = new SpotifyAuth();
    var spfPlayer = new SpotifyPlayer();
    await auth.LoadTokens(jsonPath);


    // Play a song (Only Premium + Active Device)
    // 6sdQOyqq2IDhvmx2lHOpwd   # SuperShy - NewJeans
    // 0a4MMyCrzT0En247IhqZbD   # HypeBoy - NewJeans
    string songId = "0a4MMyCrzT0En247IhqZbD";
    await spfPlayer.Play(ClientTokens.AccessToken, songId);
  }


}
