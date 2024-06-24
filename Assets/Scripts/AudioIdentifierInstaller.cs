using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;
public class AudioIdentifierInstaller
{
    // public string installationPath = "C:\\Path\\To\\Install";


    public static string InstallPath()
    {
        bool isWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
        string aWindowsLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string installationPath = isWindows ? Path.Combine(aWindowsLocalPath, "HackURythm") : Path.Combine(aWindowsLocalPath, "HackURythm");
        return installationPath;
    }

    public static string ExecutablePath()
    {
        bool isWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
        string executableFile = isWindows ? "main.exe" : "main";
        return InstallPath() + Path.DirectorySeparatorChar  + executableFile;
    }

    public static bool IsInstall(){
        return Directory.Exists(AudioIdentifierInstaller.InstallPath());
    }

    public void Install()
    {
        // msiファイルのパスを指定します
        string msiFilePath = Application.streamingAssetsPath + "/AudioIdentifier.msi";
        msiFilePath = msiFilePath.Replace("/", "\\");

        if (File.Exists(msiFilePath))
        {
            InstallMsi(msiFilePath, InstallPath());
        }
        else
        {
            UnityEngine.Debug.LogError("MSI file not found: " + msiFilePath);
        }
    }

    private void InstallMsi(string msiPath, string targetPath)
    {
        // UnityEngine.Debug.Log("Installing MSI: " + msiPath);
        // UnityEngine.Debug.Log("Installation path: " + targetPath);
        // インストーラを起動するプロセスを設定します
        Process installerProcess = new Process();
        installerProcess.StartInfo.FileName = "msiexec";
        installerProcess.StartInfo.Arguments = $"/i {msiPath} /passive TARGETDIR=\"{targetPath}\"";
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
}
