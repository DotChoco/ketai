using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Ketai.Spf.Service;
public static class Librespot{
  public static string deviceName = "SPF_Ketai";
  public static string CachePath = "D:/Github/CSharp/ketai/cache/";
  static string _args = $"-n \"{deviceName}\" -B rodio -N -c \"{CachePath}\"";
  public static bool logs = false;
  public static bool oauth = false;

  private static Process? librespotProcess;
  private static IntPtr jobHandle;

  // P/Invoke para Job Objects (Windows)
  [DllImport("kernel32.dll", SetLastError = true)]
  static extern IntPtr CreateJobObject(IntPtr lpJobAttributes, string? lpName);

  [DllImport("kernel32.dll", SetLastError = true)]
  static extern bool AssignProcessToJobObject(IntPtr hJob, IntPtr hProcess);

  [DllImport("kernel32.dll", SetLastError = true)]
  static extern bool SetInformationJobObject(IntPtr hJob, JobObjectInfoType infoType,
      IntPtr lpJobObjectInfo, uint cbJobObjectInfoLength);

  [DllImport("kernel32.dll", SetLastError = true)]
  static extern bool CloseHandle(IntPtr hObject);

  enum JobObjectInfoType
  {
      ExtendedLimitInformation = 9
  }

  [StructLayout(LayoutKind.Sequential)]
  struct JOBOBJECT_BASIC_LIMIT_INFORMATION
  {
      public long PerProcessUserTimeLimit;
      public long PerJobUserTimeLimit;
      public uint LimitFlags;
      public UIntPtr MinimumWorkingSetSize;
      public UIntPtr MaximumWorkingSetSize;
      public uint ActiveProcessLimit;
      public UIntPtr Affinity;
      public uint PriorityClass;
      public uint SchedulingClass;
  }

  [StructLayout(LayoutKind.Sequential)]
  struct IO_COUNTERS
  {
      public ulong ReadOperationCount;
      public ulong WriteOperationCount;
      public ulong OtherOperationCount;
      public ulong ReadTransferCount;
      public ulong WriteTransferCount;
      public ulong OtherTransferCount;
  }

  [StructLayout(LayoutKind.Sequential)]
  struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
  {
      public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
      public IO_COUNTERS IoInfo;
      public UIntPtr ProcessMemoryLimit;
      public UIntPtr JobMemoryLimit;
      public UIntPtr PeakProcessMemoryUsed;
      public UIntPtr PeakJobMemoryUsed;
  }

  const uint JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x2000;


  public static void Authorize(string dvcName){
    if(dvcName == string.Empty || dvcName == null)
      dvcName = "SPF_Ketai";
    deviceName = dvcName;
    _args = $"-n \"{dvcName}\" -B rodio -N -c \"{CachePath}\" --enable-oauth -R 100";
    oauth = true;
    Run();
  }


  public static void Run(){
    // Verificar que estamos en Windows
    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        throw new PlatformNotSupportedException("Este método solo está disponible en Windows");
    }

    if(!oauth)
        _args = $"-n \"{deviceName}\" -B rodio -N -c \"{CachePath}\"";

    // Crear Job Object
    jobHandle = CreateJobObject(IntPtr.Zero, null);

    if (jobHandle == IntPtr.Zero)
    {
        throw new Exception("No se pudo crear el Job Object");
    }

    // Configurar el Job Object para matar procesos al cerrar
    var info = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
        BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            LimitFlags = JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE
        }
    };

    int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
    IntPtr extendedInfoPtr = Marshal.AllocHGlobal(length);

    try
    {
        Marshal.StructureToPtr(info, extendedInfoPtr, false);

        if (!SetInformationJobObject(jobHandle, JobObjectInfoType.ExtendedLimitInformation,
            extendedInfoPtr, (uint)length))
        {
            throw new Exception("No se pudo configurar el Job Object");
        }
    }
    finally
    {
        Marshal.FreeHGlobal(extendedInfoPtr);
    }

    // Crear el proceso
    librespotProcess = new Process();
    librespotProcess.StartInfo.FileName = "librespot";
    librespotProcess.StartInfo.Arguments = _args;
    librespotProcess.StartInfo.UseShellExecute = false;
    librespotProcess.StartInfo.RedirectStandardOutput = true;
    librespotProcess.StartInfo.RedirectStandardError = true;
    librespotProcess.StartInfo.CreateNoWindow = true;

    bool isConnected = false;

    librespotProcess.ErrorDataReceived += (sender, e) => {
        if (!string.IsNullOrEmpty(e.Data)){
            if (e.Data.Contains("active device is"))
                isConnected = true;
        }
        if(logs)
            Console.WriteLine($"[Error] {e.Data}");
    };

    librespotProcess.Start();

    // Asignar el proceso al Job Object
    if (!AssignProcessToJobObject(jobHandle, librespotProcess.Handle))
    {
        throw new Exception("No se pudo asignar el proceso al Job Object");
    }

    librespotProcess.BeginOutputReadLine();
    librespotProcess.BeginErrorReadLine();

    // Esperar hasta que la conexión sea exitosa
    while (!isConnected){
        Thread.Sleep(100);
    }

    if(logs)
      Console.WriteLine("Librespot iniciado correctamente como subproceso");
  }


  public static void Stop()
  {
    if (librespotProcess != null && !librespotProcess.HasExited){
      try{
        librespotProcess.Kill();
        librespotProcess.WaitForExit(5000);
        librespotProcess.Dispose();
        librespotProcess = null;
        if(logs)
          Console.WriteLine("Librespot detenido");
      }
      catch (Exception ex){
        Console.WriteLine($"Error al detener librespot: {ex.Message}");
      }
    }

    // Cerrar el Job Object handle
    if (jobHandle != IntPtr.Zero)
    {
        CloseHandle(jobHandle);
        jobHandle = IntPtr.Zero;
    }
  }

  public static void Restart()
  {

    if(logs)
      Console.WriteLine("Reiniciando Librespot...");

    // Detener el proceso actual
    Stop();

    // Esperar un momento para asegurar que todo se limpió
    Thread.Sleep(500);

    // Reiniciar con los mismos parámetros
    Run();

    if(logs)
      Console.WriteLine("Librespot reiniciado exitosamente");
  }

  public static bool IsRunning()
  {
    return librespotProcess != null && !librespotProcess.HasExited;
  }
}
