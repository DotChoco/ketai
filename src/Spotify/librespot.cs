using System.Diagnostics;

namespace Ketai.Spf.Service;
public static class Librespot{

  public static async Task Run(){

    string _name = "Ketai2";
    string _cachePath = "D:/Github/CSharp/ketai/cache/";
    // string _args = $"-n {_name} -B rodio -N -c {_cachePath} --enable-oauth -R 100";
    string _args = $"-n \"{_name}\" -B rodio -N -c {_cachePath}";
    Process librespotProcess;
    librespotProcess = new Process();

    librespotProcess.StartInfo.FileName = "librespot";
    librespotProcess.StartInfo.Arguments = _args;
    librespotProcess.StartInfo.UseShellExecute = false;
    librespotProcess.StartInfo.RedirectStandardOutput = true;
    librespotProcess.StartInfo.RedirectStandardError = true;
    librespotProcess.StartInfo.CreateNoWindow = true;


    // Variable para indicar si la conexión fue exitosa
    bool isConnected = false;

    // Get the output
    librespotProcess.ErrorDataReceived += (sender, e) => {
      if (!string.IsNullOrEmpty(e.Data)){
        if (e.Data.Contains("active device is"))
          isConnected = true;
      }
      // Console.WriteLine($"[Error] {e.Data}");
    };

    librespotProcess.Start();
    librespotProcess.BeginOutputReadLine();
    librespotProcess.BeginErrorReadLine();

    // Wait until the conection is success
    // Wait 100 ms before to verify the conection
    while (!isConnected){
      Thread.Sleep(100);
    }

  }


}
