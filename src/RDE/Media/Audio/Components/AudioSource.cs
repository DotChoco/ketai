namespace RDE.Media.Audio;

public sealed class AudioSource{
  private string _sourcePath;
  private AudioFormat _format;

  public string SourcePath { get => _sourcePath; set => _sourcePath = SetSourcePath(value); }

  public AudioSource(){}

  public async Task AudioSourceAsync(string _path) {
    _sourcePath = _path;
    _format = AudioHeader.GetFormat(_sourcePath);
  }

  private string SetSourcePath(string _path){
    _sourcePath = _path;
    _format = AudioHeader.GetFormat(_path);
    return _path;
  }

  ///<summary>
  ///Thats the file format that will use to read the Audio File
  ///</summary>
  public AudioFormat GetFormat() => _format;
}

