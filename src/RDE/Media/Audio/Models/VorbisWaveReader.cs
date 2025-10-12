using NAudio.Wave;
using NVorbis;

public class VorbisWaveReader : WaveStream
{
    private readonly VorbisReader _vorbisReader;
    private readonly WaveFormat _waveFormat;

    public VorbisWaveReader(VorbisReader vorbisReader)
    {
        _vorbisReader = vorbisReader;
        _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_vorbisReader.SampleRate, _vorbisReader.Channels);
    }

    public override WaveFormat WaveFormat => _waveFormat;

    public override long Length => _vorbisReader.TotalSamples * _waveFormat.BlockAlign;

    public override long Position
    {
        get => _vorbisReader.SamplePosition * _waveFormat.BlockAlign;
        set => _vorbisReader.SamplePosition = value / _waveFormat.BlockAlign;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        float[] floatBuffer = new float[count / sizeof(float)];
        int samplesRead = _vorbisReader.ReadSamples(floatBuffer, 0, floatBuffer.Length);

        if (samplesRead == 0)
        {
            return 0; // Fin del archivo
        }

        // Convierte los datos de float a byte
        Buffer.BlockCopy(floatBuffer, 0, buffer, offset, samplesRead * sizeof(float));
        return samplesRead * sizeof(float);
    }
}
