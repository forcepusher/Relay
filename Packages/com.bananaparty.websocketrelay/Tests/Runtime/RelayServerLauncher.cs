using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Tests
{
    public static class RelayServerLauncher
    {
        private static Process _serverProcess;
        private static int _refCount = 0;

        public static void Start()
        {
            _refCount++;
            if (_serverProcess != null && !_serverProcess.HasExited) return;

            string scriptName = "";
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                scriptName = "LaunchServer-Windows.bat";
            else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
                scriptName = "LaunchServer-MacOS.sh";
            else
                scriptName = "LaunchServer-Linux.sh";

            string scriptPath = Path.Combine(Environment.CurrentDirectory, "Packages", "com.bananaparty.websocketrelay", "Server", scriptName);

            if (!File.Exists(scriptPath))
            {
                UnityEngine.Debug.LogError($"Relay Server script not found at: {scriptPath}");
                return;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = scriptPath,
                UseShellExecute = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(scriptPath)
            };

            try
            {
                _serverProcess = Process.Start(startInfo);
                UnityEngine.Debug.Log($"Started local relay server using {scriptName}");
                // Give the server a moment to start up
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to start Relay Server: {e.Message}");
            }
        }

        public static void Stop()
        {
            _refCount--;
            if (_refCount <= 0)
            {
                if (_serverProcess != null && !_serverProcess.HasExited)
                {
                    try
                    {
                        _serverProcess.Kill();
                        _serverProcess.Dispose();
                        UnityEngine.Debug.Log("Stopped local relay server.");
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogError($"Failed to stop Relay Server: {e.Message}");
                    }
                }
                _serverProcess = null;
                _refCount = 0;
            }
        }
    }
}
