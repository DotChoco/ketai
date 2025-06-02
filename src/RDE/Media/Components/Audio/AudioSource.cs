namespace RDE.Media.Audio;

public class AudioSource{
  protected string _sourcePath = String.Empty;

  ///<summary>
  ///Thats the file format that will use to read the Audio File
  ///</summary>
  // public AudioFormat Format = AudioFormat.MP3;
  public bool BGP = false;

  public string SourcePath { get => _sourcePath; set => _sourcePath = value; }

  public AudioSource(){}
  public AudioSource(string SourcePath) => _sourcePath = SourcePath;

}

