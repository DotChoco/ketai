namespace RDE.Media.Audio;

public sealed class AudioHeader{
  public AudioHeader(){}

  public AudioFormat IdentifyHeader(string path){
    byte[] header = GetHeader(path);
    if(IsMp3(header)){ return AudioFormat.MP3; }
    if(IsWav(header)){ return AudioFormat.WAV; }
    if(IsOgg(header)){ return AudioFormat.OGG; }
    if(IsFlac(header)){ return AudioFormat.FLAC; }
    return AudioFormat.None;
  }

  private byte[] GetHeader(string path){
    return Array.Empty<byte>();
  }



  private bool IsMp3(byte[] header){
    return true;
  }


   private bool IsWav(byte[] header){
    return true;
  }

   private bool IsOgg(byte[] header){
    return true;
  }

   private bool IsFlac(byte[] header){
    return true;
  }




}
