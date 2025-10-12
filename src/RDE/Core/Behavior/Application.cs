using System;
using System.Text.Json;
using System.IO;

namespace RDE.Core.Behavior;

public sealed class BuildConfig{
  public BuildMode BuildMode { get; set; }
}


public class Application {

  // Path from the Game is Execute
  private static string _path = AppDomain.CurrentDomain.BaseDirectory;
  public static string Path { get => _path; }


  // Path from your csharp project
  private static string _projectPath = $@"{Environment.CurrentDirectory}\";


  // Path that game get the Assets
  private static string _assetsPath = _projectPath + "assets";
  public static string AssetsPath { get => _assetsPath; }


  // Path that use the engine to save the assets when the project will build's and run
  private static string _export_Path = $@"{_path}assets";


  // Modes to define how and where the asssets will read's
  private static BuildMode _buildMode = BuildMode.NONE;
  public static BuildMode BuildMode { get => _buildMode; }


  public static void Init(){
    BuildConfig bc = new(){ BuildMode = BuildMode.BUILD_R };
    string data = string.Empty;
    if(File.Exists(_projectPath + "BUILD")){
      StreamReader sr = new(_projectPath + "BUILD");
      bc = JsonSerializer.Deserialize<BuildConfig>(sr.ReadToEnd());
      _buildMode = bc.BuildMode;
    }
  }


  public static void SetBuildMode(BuildMode bm)  {
    _buildMode = bm;
    if(_buildMode == BuildMode.BUILD_D || _buildMode == BuildMode.BUILD_R)
      ExportAssets();
  }

  private static void ExportAssets(){
    if(!Directory.Exists(_export_Path))
      Directory.CreateDirectory(_export_Path);

    CopyAssetsToBuild(_assetsPath, _export_Path);
  }

  static void CopyAssetsToBuild(string origin, string destiny) {
    // Create the directory if doesn't exists
    if(!Directory.Exists(destiny))
      Directory.CreateDirectory(destiny);

    // Copy all the file's from the current path
    foreach (string file in Directory.GetFiles(origin)) {
      string fileName = System.IO.Path.GetFileName(file);
      string destinyArchivo = System.IO.Path.Combine(destiny, fileName);
      File.Copy(file, destinyArchivo, overwrite: true); // Sobrescribe si existe
    }

    // Copy file's using recursive method
    foreach (string subDir in Directory.GetDirectories(origin)) {
      string nameSubDir = System.IO.Path.GetFileName(subDir);
      string destinySubDir = System.IO.Path.Combine(destiny, nameSubDir);
      CopyAssetsToBuild(subDir, destinySubDir);
    }
  }

}
