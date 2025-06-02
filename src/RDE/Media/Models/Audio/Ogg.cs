using NVorbis;
using NAudio.Wave;

namespace RDE.Media.Audio;

public sealed class Ogg: GuideLines{
public Ogg(){}

public void Play(string SourcePath){
  _sourcePath = SourcePath;
    // Usa NVorbis para leer el archivo .ogg
    var vorbisReader = new VorbisReader(SourcePath);

    // Convierte el archivo .ogg a un formato que NAudio pueda manejar
    // var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(vorbisReader.SampleRate, vorbisReader.Channels);
    var audioFile = new VorbisWaveReader(vorbisReader);

    // Reproduce el audio usando NAudio
    var outputDevice = new WaveOutEvent();
    outputDevice.Init(audioFile);
    outputDevice.Play();

    Console.WriteLine("Reproduciendo archivo .ogg... Presiona Enter para detener.");
    Thread.Sleep(audioFile.TotalTime);
    // Console.WriteLine(audioFile.TotalTime);
}


}
