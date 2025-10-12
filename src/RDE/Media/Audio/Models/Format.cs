using RDE.Core.Logs;
using System;
using NAudio.Wave;

namespace RDE.Media.Audio;

public abstract class Format{
public TimeSpan CurrentTime { get; }
public TimeSpan TotalTime { get; }
public Log Log { get; }

protected TimeSpan _currentTime { get; }
protected string _sourcePath = string.Empty;
protected Log _log { get; } = new();
protected IWavePlayer outputDevice;
protected WaveStream _audioFile;

public bool IsPlaying => outputDevice?.PlaybackState == PlaybackState.Playing;
public bool IsPaused => outputDevice?.PlaybackState == PlaybackState.Paused;


public Format IncrementVol(float vol){
  if(outputDevice.Volume + vol >= 1)
    outputDevice.Volume = 1;
  else
    outputDevice.Volume += vol;
return this;
}

public Format DecrementVol(float vol){
  if(outputDevice.Volume - vol <= 0)
    outputDevice.Volume = 0;
  else
    outputDevice.Volume -= vol;
return this;
}

public Format Reset(){
  outputDevice.Pause();
  _audioFile.CurrentTime = TimeSpan.FromSeconds(0);
  outputDevice.Dispose();
  outputDevice.Init(_audioFile);
return this;
}

public Format Pause() {
  if (IsPlaying)
    outputDevice.Pause();
return this;
}

public Format Stop() {
  outputDevice?.Stop();
  _audioFile?.Dispose();
  outputDevice?.Dispose();
  _audioFile = null;
  outputDevice = null;
return this;
}

public Format Backward(int seconds){
  if (IsPlaying && outputDevice != null){
    outputDevice.Pause();
    _audioFile.Skip(seconds * -1);
    outputDevice.Play();
  }
return this;
}
public Format Forward(int seconds)
{
  if (_audioFile.CurrentTime >= _audioFile.TotalTime.Subtract(TimeSpan.FromSeconds(10)))
    Reset();
  if (IsPlaying && outputDevice != null)
  {
    outputDevice.Pause();
    _audioFile.Skip(seconds);
    outputDevice.Play();
  }
return this;
}

public Format Play(){
  if (IsPaused || outputDevice.PlaybackState == PlaybackState.Stopped)
    outputDevice.Play();
return this;
}


}
