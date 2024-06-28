using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;
public class AudioIdentifierInstaller
{
    // public string installationPath = "C:\\Path\\To\\Install";


    public static string InstallPath()
    {
        string aWindowsLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string installationPath = Path.Combine(aWindowsLocalPath, "HackURythm");
        return installationPath;
    }

    public static string ExecutablePath()
    {
        bool isWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
        string executableFile = isWindows ? "main.exe" : $"AudioIdentifier_for_hacku.app/Contents/MacOS/main";
        return InstallPath() + Path.DirectorySeparatorChar + executableFile;
    }

    public static bool IsInstall()
    {
        return Directory.Exists(AudioIdentifierInstaller.InstallPath());
    }

    public void Install()
    {
        // msiファイルのパスを指定します
        bool isWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
        string installerPath = Application.streamingAssetsPath + (isWindows ? "/AudioIdentifier.msi" : "/AudioIdentifier_Installer.dmg");

        if (File.Exists(installerPath))
        {
            if (isWindows)
            {
                installerPath = installerPath.Replace("/", "\\");
                InstallMsi(installerPath, InstallPath());
            }
            else
            {
                InstallDmg(installerPath, InstallPath());
            }
        }
        else
        {
            UnityEngine.Debug.LogError("MSI file not found: " + installerPath);
        }
    }

    private void InstallMsi(string msiPath, string targetPath)
    {
        // UnityEngine.Debug.Log("Installing MSI: " + msiPath);
        // UnityEngine.Debug.Log("Installation path: " + targetPath);
        // インストーラを起動するプロセスを設定します
        Process installerProcess = new Process();
        installerProcess.StartInfo.FileName = "msiexec";
        installerProcess.StartInfo.Arguments = $"/i \"{msiPath}\" /passive TARGETDIR=\"{targetPath}\"";
        installerProcess.StartInfo.UseShellExecute = true;
        installerProcess.StartInfo.RedirectStandardOutput = false;
        installerProcess.StartInfo.RedirectStandardError = false;

        installerProcess.Start();
        installerProcess.WaitForExit();

        if (installerProcess.ExitCode == 0)
        {
            UnityEngine.Debug.Log("Installation successful.");
        }
        else
        {
            UnityEngine.Debug.LogError("Installation failed. Exit code: " + installerProcess.ExitCode);
        }
    }

    private void InstallDmg(string dmgPath, string targetPath)
    {
        // dmgファイルをマウントするコマンドを設定します
        Process mountProcess = new Process();
        mountProcess.StartInfo.FileName = "hdiutil";
        mountProcess.StartInfo.Arguments = $"attach \"{dmgPath}\"";
        mountProcess.StartInfo.RedirectStandardOutput = true;
        mountProcess.StartInfo.RedirectStandardError = true;
        mountProcess.StartInfo.UseShellExecute = false;

        mountProcess.Start();
        mountProcess.WaitForExit();

        if (mountProcess.ExitCode == 0)
        {
            string volumePath = GetMountedVolumePath(mountProcess.StandardOutput.ReadToEnd());
            if (string.IsNullOrEmpty(volumePath)) {
                 UnityEngine.Debug.LogError("Failed to find mounted volume path.");
                 return;
            }

            // アプリケーションをコピーするコマンドを設定します
            Process copyProcess = new Process();
            copyProcess.StartInfo.FileName = "cp";
            copyProcess.StartInfo.Arguments = $"-R \"{volumePath}\" \"{targetPath}\"";
            copyProcess.StartInfo.RedirectStandardOutput = true;
            copyProcess.StartInfo.RedirectStandardError = true;
            copyProcess.StartInfo.UseShellExecute = false;

            copyProcess.Start();
            copyProcess.WaitForExit();

            if (copyProcess.ExitCode != 0){
                UnityEngine.Debug.LogError("Failed to copy application. Exit code: " + copyProcess.ExitCode);
                return;
            }
            
            UnityEngine.Debug.Log("Installation successful.");

            // dmgファイルをアンマウントするコマンドを設定します
            Process unmountProcess = new Process();
            unmountProcess.StartInfo.FileName = "hdiutil";
            unmountProcess.StartInfo.Arguments = $"detach \"{volumePath}\"";
            unmountProcess.StartInfo.RedirectStandardOutput = true;
            unmountProcess.StartInfo.RedirectStandardError = true;
            unmountProcess.StartInfo.UseShellExecute = false;

            unmountProcess.Start();
            unmountProcess.WaitForExit();

            if (unmountProcess.ExitCode != 0)
            {
                UnityEngine.Debug.LogError("Failed to unmount dmg.");
            }
        } else {
            UnityEngine.Debug.LogError("Failed to mount dmg. Exit code: " + mountProcess.ExitCode);
        }
    }

    string GetMountedVolumePath(string output)
    {
        // マウントされたボリュームのパスを取得する処理を記述します
        // 出力を解析してボリュームパスを取得する例
        using (StringReader reader = new StringReader(output))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("/Volumes/"))
                {
                    return line.Substring(line.IndexOf("/Volumes/")).Trim();
                }
            }
        }

        return string.Empty;
    }
}
