using System.IO;
using System.Diagnostics;
using UnityEngine;

public class JsonStream
{
    private string GetStream(string cmd = "", string args = "")
    {
        ProcessStartInfo start = new ProcessStartInfo();

        //full path to python.exe
        start.FileName = "/Library/Frameworks/Python.framework/Versions/3.6/bin/python3";

        //full path to directory containing the .py file
        start.WorkingDirectory = Directory.GetCurrentDirectory() + "/Assets/googlesheetspyprojectbatch";

        //python filename
        start.Arguments = "tostream.py";

        start.UseShellExecute = false;
        start.RedirectStandardInput = true;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        start.CreateNoWindow = false; 

        using (Process process = Process.Start(start))
        {
            string output = process.StandardOutput.ReadToEnd();
            //string error = process.StandardError.ReadToEnd();
            process.WaitForExit();
            return output;
        }
    }


    public void DoEverythingStream()
    {
        string jsonText = GetStream();
        if (string.IsNullOrEmpty(jsonText) == false)
        {
            string[] games = jsonText.Trim().Split('\n');
            foreach (string game in games)
            {
                UnityEngine.Debug.Log(game);
            }
        }
        else
        {
            UnityEngine.Debug.Log("empty");
        }
    }
}
