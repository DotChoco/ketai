namespace RDE.Media.Audio;
using RDE.Core.Logs;


public sealed class AudioPlayer{

  private Mp3 mp3 = new();
  private Wav wav = new();
  private Ogg ogg = new();
  private Flac flac = new();
  private AudioHeader _audioHeader = new();

  ///<summary>
  ///Thats the file format that will use to read the Audio File
  ///</summary>
  private AudioFormat _format;


  public Log Log = new();
  public AudioSource source = new();

  public Log Play() => PlayAudio();
  // public Log Pause() => PauseAudio();
  // public Log Stop() => StopAudio();
  // public Log Resume() => ResumeAudio();

  private Log PlayAudio(){
    // Verify if the file exists
    if (!File.Exists(source.SourcePath)) {
      Log.content=$"The \"{source.SourcePath}\" doesn't exists";
      return Log;
    }
    // _format = _audioHeader.IdentifyHeader(source.SourcePath);
    _format = AudioFormat.OGG;


    // Play the audio if the source is mp3
    if(_format == AudioFormat.MP3){
      mp3.BGP = source.BGP;
      mp3.Play(source.SourcePath);
      Log = mp3.Log;
    }

    // Play the audio if the source is ogg
    else if(_format == AudioFormat.OGG){
      ogg.BGP = source.BGP;
      ogg.Play(source.SourcePath);
      Log = ogg.Log;
    }

    // Play the audio if the source is flac
    else if(_format == AudioFormat.FLAC){
      flac.BGP = source.BGP;
      flac.Play(source.SourcePath);
      Log = flac.Log;
    }

    // Play the audio if the source is wav
    else if(_format == AudioFormat.WAV){
      wav.BGP = source.BGP;
      wav.Play(source.SourcePath);
      Log = wav.Log;
    }

    return Log;
  }









}
