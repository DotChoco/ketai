using System.IO;
namespace RDE.Core.Logs;

public class Log{
  public string content;

  public string FileExists(string source){
     if(source == null) {
      return "The path is empty";
    }

    // Verify if the file exists
    if (!File.Exists(source)) {
      return $"The \"{source}\" doesn't exists";
    }
    return "General Error";
  }
}
