using Ketai.Spf.Auth;
using Ketai.Spf.Player;

namespace Ketai.Spf.Testing;

public sealed class SpfTest{

  public async Task Auth(){
    var auth = new SpotifyAuth();

    // Create new AccessToken
    // string code = await auth.GetCodeFromSpotify();
    // await auth.GetTokens(code);


    // Update AccessToken
    await auth.RefreshAccessToken();
  }

  public async Task Play(){
    var spfPlayer = new SpotifyPlayer();


    // Play a song (Only Premium + Active Device)
    // 56v8WEnGzLByGsDAXDiv4d   # ETA - NewJeans
    // 5sdQOyqq2IDhvmx2lHOpwd   # SuperShy - NewJeans
    // 0a4MMyCrzT0En247IhqZbD   # HypeBoy - NewJeans
    // 65FftemJ1DbbZ45DUfHJXE   # OMG - NewJeans


    string songId = "0a4MMyCrzT0En247IhqZbD";
    await spfPlayer.Play(ClientTokens.AccessToken, songId);
  }


}
