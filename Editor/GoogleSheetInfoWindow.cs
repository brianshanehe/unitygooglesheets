using System.IO;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using UnityEditorInternal;

public class GoogleSheetInfoWindow : EditorWindow {
    
    static string sourceSpreadsheetId;
    static string sheetFolderId;
    static string pythonStreamCode;
    static string pythonStreamPath;
    static string pythonFileCode;
    static string pythonFilePath;
    static string tempSpreadsheetId;
    static string tempFolderId;

    public GoogleSheetInfoWindow()
    {
        pythonStreamPath = "Assets/googlesheetspyprojectbatch/tostream.py";
        pythonStreamCode = ReadData(pythonStreamPath);
        pythonFilePath = "Assets/googlesheetspyprojectbatch/tofile.py";
        pythonFileCode = ReadData(pythonFilePath);
        tempSpreadsheetId = sourceSpreadsheetId;
        tempFolderId = sheetFolderId;
    }

    [MenuItem("Google Sheets/Set Info")]
    public static void ShowWindow(){
        GetWindow<GoogleSheetInfoWindow>("Sheet Info");
    }


    [MenuItem("Google Sheets/Print Info")]
    static void PrintInfo()
    {
        Debug.Log("Spreadsheet ID: " + sourceSpreadsheetId);
        Debug.Log("Folder ID: " + sheetFolderId);
    }

    void OnGUI()
    {
        string pattern = "^\"[\\w\\d-_]+\"$";
        Regex regexPattern = new Regex(pattern);
        GUILayout.BeginVertical();
        //window code
        GUILayout.Label("Enter the source folder id, enclosed by double quotes");
        tempFolderId = EditorGUILayout.TextField("", tempFolderId);
        GUILayout.Space(10f);
        GUILayout.Label("Enter the source spreadsheet id, enclosed by double quotes");
        tempSpreadsheetId = EditorGUILayout.TextField("", tempSpreadsheetId);

        if (GUILayout.Button("Submit"))
        {
            if (regexPattern.IsMatch(tempFolderId)
                && regexPattern.IsMatch(tempSpreadsheetId))
            {
                Debug.Log("Successfully changed the Folder ID and Spreadsheet ID");
                WriteStreamData(pythonStreamPath);
                WriteFileData(pythonFilePath);
                pythonStreamCode = ReadData(pythonStreamPath);
                pythonFileCode = ReadData(pythonFilePath);
            }
            else
            {
                Debug.Log("Make sure the id is valid and enclosed in double quotes.");
            }
        }
        GUILayout.EndVertical();

    }



    //code to read info
    static string ReadData(string path)
    {
        if (File.Exists(path))
        {
            string pyCode = File.ReadAllText(path);
            string[] pyCodeSplit = pyCode.Split('\n', '\t');
            foreach (string pyLine in pyCodeSplit)
            {
                string tempString = pyLine.Trim();
                if (string.IsNullOrEmpty(tempString) == false)
                {
                    if (tempString.StartsWith("SRC_SHEET_ID = ", System.StringComparison.CurrentCulture))
                    {
                        int lastLocation = tempString.IndexOf("\"", System.StringComparison.CurrentCulture);
                        sourceSpreadsheetId = tempString.Substring(lastLocation);
                    }
                    else if (tempString.StartsWith("SHEET_FOLDER_ID = ", System.StringComparison.CurrentCulture))
                    {
                        int lastLocation = tempString.IndexOf("\"", System.StringComparison.CurrentCulture);
                        sheetFolderId = tempString.Substring(lastLocation);
                    }
                }
            }
            return pyCode;
        }
        else
        {
            Debug.LogError(path + " does not exist");
            return "";
        }
    }


    //code to write info
    static void WriteStreamData(string path)
    { 
        if (File.Exists(path))
        {
            pythonStreamCode = pythonStreamCode.Replace(sourceSpreadsheetId, tempSpreadsheetId);
            pythonStreamCode = pythonStreamCode.Replace(sheetFolderId, tempFolderId);
            File.WriteAllText(path, pythonStreamCode);
        }
        else
        {
            Debug.LogError(path + " does not exist");
        }
    }

    static void WriteFileData(string path)
    {
        if (File.Exists(path))
        {
            pythonFileCode = pythonFileCode.Replace(sourceSpreadsheetId, tempSpreadsheetId);
            pythonFileCode = pythonFileCode.Replace(sheetFolderId, tempFolderId);
            File.WriteAllText(path, pythonFileCode);
        }
        else
        {
            Debug.LogError(path + " does not exist");
        }
    }





}