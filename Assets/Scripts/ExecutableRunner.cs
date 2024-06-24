using System.Diagnostics;

/// <summary>
/// 実行可能ファイルを実行するクラス
/// </summary>
public class ExecutableRunner
{

    private Process aProcess;

    /// <summary>
    /// 実行可能ファイルを非同期で実行します
    /// </summary>
    // <param name="executablePath">実行可能ファイルのパスparam>
    // <param name="args">スクリプトに渡す引数</param>
    public static ExecutableRunner Run (string executablePath, string[] args){
        ExecutableRunner anExecutableRunner = new ExecutableRunner();
        anExecutableRunner.RunExecutable(executablePath, args);
        return anExecutableRunner;
    }

    /// <summary>
    /// 実行可能ファイルを非同期で実行します
    /// </summary>
    /// <param name="executablePath">実行可能ファイルのパスparam>
    /// <param name="args">スクリプトに渡す引数</param>
    public void RunExecutable(string executablePath, string[] args)
    {
        ProcessStartInfo aProcessStartInfo = new ProcessStartInfo()
        {
            FileName = executablePath,
            UseShellExecute = true,
            CreateNoWindow = true,
            RedirectStandardOutput = false,
            // WorkingDirectory = WorkingDirectory,
            Arguments = string.Join(" ", args),
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
}