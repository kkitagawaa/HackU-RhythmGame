using System.Diagnostics;
using System.IO;
using UnityEngine;

/// <summary>
/// Pythonスクリプトを実行するクラス
/// </summary>
public class PythonRunner
{

    private Process aProcess;

    /// <summary>
    /// Pythonスクリプトを非同期で実行します
    /// </summary>
    // <param name="scriptPath">スクリプトのパス</param>
    // <param name="args">スクリプトに渡す引数</param>
    public static PythonRunner Run (string WorkingDirectory, string scriptPath, string[] args){
        PythonRunner aPythonRunner = new PythonRunner();
        aPythonRunner.RunPython(WorkingDirectory, scriptPath, args);
        return aPythonRunner;
    }

    /// <summary>
    /// Pythonスクリプトを非同期で実行します
    /// </summary>
    /// <param name="scriptPath">スクリプトのパス</param>
    /// <param name="args">スクリプトに渡す引数</param>
    public void RunPython(string WorkingDirectory, string scriptPath, string[] args)
    {
        string aPythonPath = this.GetPythonPath();
        ProcessStartInfo aProcessStartInfo = new ProcessStartInfo()
        {
            FileName = aPythonPath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = false,
            WorkingDirectory = WorkingDirectory,
            Arguments = scriptPath + " " + string.Join(" ", args),
        };
        this.aProcess = Process.Start(aProcessStartInfo);
    }

    /// <summary>
    /// Pythonスクリプトを停止します
    /// </summary>
    public void Stop()
    {
        this.aProcess?.Kill();
    }

    /// <summary>
    /// 環境に合わせた、Pythonの実行ファイルのパスを取得します
    /// </summary>
    /// <returns>Pythonの実行ファイルのパス</returns>
    private string GetPythonPath()
    {
        bool isWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
        string aFindCommand = isWindows ? "/c where python" : "which python";
        string anExecutablePath = isWindows ? "cmd.exe" : "/bin/bash";

        ProcessStartInfo aProcessStartInfo = new ProcessStartInfo()
        {
            FileName = anExecutablePath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            Arguments = aFindCommand
        };
        Process aProcess = Process.Start(aProcessStartInfo);

        StreamReader aStreamReader = aProcess.StandardOutput;
        string aStandardOutputString = aStreamReader.ReadLine();

        aProcess.WaitForExit();
        aProcess.Close();

        // aStandardOutputStringの末尾に拡張子がついていない場合、拡張子を付与する
        if (isWindows && (!aStandardOutputString.EndsWith(".exe") || !aStandardOutputString.EndsWith(".bat")))
        {
            aStandardOutputString += ".bat";
        }

        return aStandardOutputString;
    }
}