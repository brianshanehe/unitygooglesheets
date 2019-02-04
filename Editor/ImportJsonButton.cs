using UnityEditor;

public class ImportJsonButton{
    [MenuItem("Google Sheets/Import Stream")]
    static void ImportStream()
    {
        JsonStream jsonStream = new JsonStream();
        jsonStream.DoEverythingStream();
    }

    [MenuItem("Google Sheets/Import File")]
    static void ImportFile()
    {
        JsonToFile jsonToFile = new JsonToFile();
        jsonToFile.DoEverythingFile();
    }

}
