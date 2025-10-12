using RDE.Core.Logs;
// using System.IO;
// using System;
namespace RDE.Media.Audio;

public sealed class AudioPlayer{

  private Mp3 mp3 = new();
  private Wav wav = new();
  private Ogg ogg = new();
  private Flac flac = new();

  ///<summary>
  ///Thats the file format that will use to read the Audio File
  ///</summary>
  private AudioFormat _format;

  public Log DataLog = new();
  public AudioSource? source = new();

  public Log Play() => PlayAudio();
  public async Task LoadAsync(AudioSource? src) => Init(src);
  public Log Pause() => PauseAudio();
  public Log Stop() => StopAudio();
  public void moreVol(float vol) { ogg.IncrementVol(vol);}
  public void lessVol(float vol) { ogg.DecrementVol(vol);}
  bool IsPlaying = false;


  public AudioPlayer(){}

  public void Init(AudioSource? src = default){
    if(source == null && src == null)
      return;

    source = src;

    //Extract AudioFormat from FileHeader
    _format = source.GetFormat();

    //Check if the file exists and isn't null string
    DataLog.content = DataLog.FileExists(source.SourcePath);

    if(_format == AudioFormat.None)
      DataLog.content = "The audio can't be reconozited";

    // Play the audio if the source is mp3
    if(_format == AudioFormat.MP3)
      mp3.LoadAudio(source.SourcePath);


    // Play the audio if the source is ogg
    else if(_format == AudioFormat.OGG)
      ogg.LoadAudio(source.SourcePath);


    // Play the audio if the source is flac
    else if(_format == AudioFormat.FLAC)
      flac.LoadAudio(source.SourcePath);


    // Play the audio if the source is wav
    else if(_format == AudioFormat.WAV)
      wav.LoadAudio(source.SourcePath);

  }

  public async Task<Log> PlayAsync(){
    var log = PlayAudio();

    // Esperar hasta que termine
    await Task.Run(() =>{
      while (IsPlaying){ // propiedad que controlas con NAudio
        if(_format == AudioFormat.MP3)
          IsPlaying = mp3.IsPlaying;

        else if(_format == AudioFormat.OGG)
          IsPlaying = ogg.IsPlaying;

        else if(_format == AudioFormat.FLAC)
          IsPlaying = flac.IsPlaying;

        else if(_format == AudioFormat.WAV)
          IsPlaying = wav.IsPlaying;

        Thread.Sleep(200);
      }
    });

    return log;
  }

  private Log PlayAudio(){

    // Play the audio if the source is mp3
    if(_format == AudioFormat.MP3){
      IsPlaying = mp3.Play().IsPlaying;
      Console.WriteLine("Played");
      return mp3.Log;
    }

    // Play the audio if the source is ogg
    else if(_format == AudioFormat.OGG){
      IsPlaying = ogg.Play().IsPlaying;
      return ogg.Log;
    }

    // Play the audio if the source is flac
    else if(_format == AudioFormat.FLAC){
      IsPlaying = flac.Play().IsPlaying;
      return flac.Log;
    }

    // Play the audio if the source is wav
    else if(_format == AudioFormat.WAV){
      IsPlaying = wav.Play().IsPlaying;
      return wav.Log;
    }

    return DataLog;
  }


  private Log PauseAudio(){
    if(_format == AudioFormat.OGG){
      ogg.Pause();
      IsPlaying = ogg.IsPlaying;
      DataLog = ogg.Log;
    }
    if(_format == AudioFormat.FLAC){
      flac.Pause();
      IsPlaying = flac.IsPlaying;
      DataLog = flac.Log;
    }
    if(_format == AudioFormat.WAV){
      wav.Pause();
      IsPlaying = wav.IsPlaying;
      DataLog = wav.Log;
    }
    if(_format == AudioFormat.OGG){
      mp3.Pause();
      IsPlaying = mp3.IsPlaying;
      DataLog = mp3.Log;
    }
    return DataLog;
  }

  private Log StopAudio(){
    if(_format == AudioFormat.OGG){
      ogg.Stop();
      IsPlaying = ogg.IsPlaying;
      DataLog = ogg.Log;
    }
    if(_format == AudioFormat.FLAC){
      flac.Stop();
      IsPlaying = flac.IsPlaying;
      DataLog = flac.Log;
    }
    if(_format == AudioFormat.WAV){
      wav.Stop();
      IsPlaying = wav.IsPlaying;
      DataLog = wav.Log;
    }
    if(_format == AudioFormat.OGG){
      mp3.Stop();
      IsPlaying = mp3.IsPlaying;
      DataLog = mp3.Log;
    }
    return DataLog;
  }


  public void Forward(int time){
    if(IsPlaying){
      if(_format == AudioFormat.OGG)
        ogg.Forward(time);

      if(_format == AudioFormat.WAV)
        wav.Forward(time);

      if(_format == AudioFormat.FLAC)
        flac.Forward(time);

      if(_format == AudioFormat.MP3)
        mp3.Forward(time);
    }
  }
  public void Backward(int time){
    if(IsPlaying){
      if(_format == AudioFormat.OGG)
        ogg.Backward(time);
    if(_format == AudioFormat.WAV)
        wav.Backward(time);

    if(_format == AudioFormat.FLAC)
        flac.Backward(time);

    if(_format == AudioFormat.MP3)
        mp3.Backward(time);
    }
  }




}
