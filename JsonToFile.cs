using System.IO;
using System.Diagnostics;
using UnityEngine;

public class JsonToFile
{
    private void RunCmd(string cmd = "", string args = "")
    {
        ProcessStartInfo start = new ProcessStartInfo();

        //full path to python.exe
        start.FileName = "/Library/Frameworks/Python.framework/Versions/3.6/bin/python3";

        //full path to directory containing the .py file
        start.WorkingDirectory = Directory.GetCurrentDirectory() + "/Assets/googlesheetspyprojectbatch";

        //python filename
        start.Arguments = "tofile.py";

       
        start.UseShellExecute = false;
        start.RedirectStandardInput = true;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        start.CreateNoWindow = false;

        using (Process process = Process.Start(start))
        {
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            if (output.StartsWith("Encountered Error: ", System.StringComparison.CurrentCulture))
            {
                UnityEngine.Debug.Log(output);
            }
        }
    }


    public void DoEverythingFile()
    {
        string dataPath = "Assets/googlesheetspyprojectbatch/json_data.txt";
        try
        {
            if (File.Exists(dataPath))
            {
                File.Delete(dataPath);
            }

            // Create the file.
            using (FileStream fs = File.Create(dataPath))
            {
            }
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.Log(ex.ToString());
        }
        RunCmd();
        string jsonText = File.ReadAllText(dataPath);
        if (string.IsNullOrEmpty(jsonText) == false)
        {
            string[] games = jsonText.Trim().Split('\n');
            foreach (string game in games)
            {
                UnityEngine.Debug.Log(game);
            }
        }
        
    }
}
