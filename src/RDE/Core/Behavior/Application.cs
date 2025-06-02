using System;
using System.IO;

namespace RDE.Behavior;

public class Application {
    private static string _gamePath = $@"{Environment.CurrentDirectory}\";
    private static string _exportPath = @"bin\Debug\net9.0\";


    //Path from the Game is Execute
    private static string path { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
    public static string Path { get => path; }


    //Path that game get the Assets
    private static string _assetsPath = _gamePath + "assets";
    public static string AssetsPath { get => _assetsPath; }

    public static void ExportAssets(){
        string _exportAssetsPath = _gamePath + _exportPath + "assets";

        if(!Directory.Exists(_exportAssetsPath))
            Directory.CreateDirectory(_exportAssetsPath);
        try
        {
            CopiarEstructuraCompleta(AssetsPath, _exportAssetsPath);
            _assetsPath = _exportAssetsPath;
            // Console.WriteLine("¡Copia exitosa!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    
    static void CopiarEstructuraCompleta(string origen, string destino)
    {
        // Crear el directorio de destino si no existe
        Directory.CreateDirectory(destino);

        // Copiar todos los archivos del directorio actual
        foreach (string archivo in Directory.GetFiles(origen))
        {
            string nombreArchivo = System.IO.Path.GetFileName(archivo);
            string destinoArchivo = System.IO.Path.Combine(destino, nombreArchivo);
            File.Copy(archivo, destinoArchivo, overwrite: true); // Sobrescribe si existe
        }

        // Copiar subdirectorios recursivamente
        foreach (string subDir in Directory.GetDirectories(origen)) {
            string nombreSubDir = System.IO.Path.GetFileName(subDir);
            string destinoSubDir = System.IO.Path.Combine(destino, nombreSubDir);
            CopiarEstructuraCompleta(subDir, destinoSubDir); // Llamada recursiva
        }
    }

} 