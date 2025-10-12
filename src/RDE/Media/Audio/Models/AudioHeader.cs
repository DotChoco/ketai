using System.IO;

namespace RDE.Media.Audio;

public static class AudioHeader{

  public static AudioFormat GetFormat(string path){
    if(path == string.Empty && path == null)
      return AudioFormat.None;

    byte[] data = File.ReadAllBytes(path);

    if(IsMp3(data)){ return AudioFormat.MP3; }
    if(IsWav(data)){ return AudioFormat.WAV; }
    if(IsOgg(data)){ return AudioFormat.OGG; }
    if(IsFlac(data)){ return AudioFormat.FLAC; }

    return AudioFormat.None;
  }


  private static bool IsMp3(byte[] dataBytes){
    return dataBytes[0] == 0x49 && dataBytes[1] == 0x44 && dataBytes[2] == 0x33;
  }


   private static bool IsWav(byte[] dataBytes){
    return dataBytes[0] == 0x52 && dataBytes[1] == 0x49 && dataBytes[2] == 0x46 && dataBytes[3] == 0x46 && dataBytes[8] == 0x57 && dataBytes[9] == 0x41 && dataBytes[10] == 0x56 && dataBytes[11] == 0x45;
  }

   private static bool IsOgg(byte[] dataBytes){
    return dataBytes[0] == 0x4F && dataBytes[1] == 0x67 && dataBytes[2] == 0x67 && dataBytes[3] == 0x53;
  }

   private static bool IsFlac(byte[] dataBytes){
    return dataBytes[0] == 0x66 && dataBytes[1] == 0x4C && dataBytes[2] == 0x61 && dataBytes[3] == 0x43;
  }




}
