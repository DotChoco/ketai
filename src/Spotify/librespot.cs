using System.Diagnostics;

namespace Ketai.Spf.Service;
public static class Librespot{

  public static void Run(){

    string args = "--name \"Ketai 2\" --backend rodio  --enable-volume-normalisation --quiet";
    Process librespotProcess;
    librespotProcess = new Process();

    librespotProcess.StartInfo.FileName = "librespot";
    librespotProcess.StartInfo.Arguments = args;
    librespotProcess.StartInfo.UseShellExecute = false;
    librespotProcess.StartInfo.RedirectStandardOutput = true;
    librespotProcess.StartInfo.RedirectStandardError = true;
    librespotProcess.StartInfo.CreateNoWindow = true;

    // Capturar output
    librespotProcess.OutputDataReceived += (sender, e) => {
      if (!string.IsNullOrEmpty(e.Data))
          Console.WriteLine($"[Librespot] {e.Data}");
    };

    librespotProcess.ErrorDataReceived += (sender, e) => {
      if (!string.IsNullOrEmpty(e.Data))
          Console.WriteLine($"[Error] {e.Data}");
    };

    librespotProcess.Start();
    librespotProcess.BeginOutputReadLine();
    librespotProcess.BeginErrorReadLine();

  }


}
