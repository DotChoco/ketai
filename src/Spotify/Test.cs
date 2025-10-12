using Ketai.Spf.Auth;
using Ketai.Spf.Player;

namespace Ketai.Spf.Testing;

public sealed class SpfTest{

  public async Task Auth(){
    var auth = new SpotifyAuth();
    var spfPlayer = new SpotifyPlayer();

    // Create new AccessToken
    // string code = await auth.GetCodeFromSpotify();
    // await auth.GetTokens(code);


    // Update AccessToken
    await auth.RefreshAccessToken();

    // Play a song (Only Premium + Active Device)
    // 5sdQOyqq2IDhvmx2lHOpwd   # SuperShy - NewJeans
    // 0a4MMyCrzT0En247IhqZbD   # HypeBoy - NewJeans
    // 65FftemJ1DbbZ45DUfHJXE   # OMG - NewJeans
    string songId = "5sdQOyqq2IDhvmx2lHOpwd";
    await spfPlayer.Play(ClientTokens.AccessToken, songId);
  }


}
