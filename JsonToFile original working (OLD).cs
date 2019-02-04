using System.IO;
using System.Diagnostics;
using UnityEngine;

public class JsonToFileOrig
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
        }
    }


    public void DoEverythingFile()
    {
        RunCmd();
        string data_path = "Assets/googlesheetspyprojectbatch/json_data.txt";
        if (File.Exists(data_path))
        {
            string json_text = File.ReadAllText(data_path);
            string[] games = json_text.Trim().Split('\n');
            foreach (string game in games)
            {
                UnityEngine.Debug.Log(game);
            }
        }
        else
        {
            UnityEngine.Debug.LogError("file not yet created or invalid json_data.txt path");
        }
    }
}
