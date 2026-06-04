using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
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
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(scriptPath)
            };

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/c \"{scriptPath}\"";
                startInfo.UseShellExecute = false;
            }
            else
            {
                startInfo.FileName = "/bin/bash";
                startInfo.Arguments = $"\"{scriptPath}\"";
                startInfo.UseShellExecute = false;
            }

            try
            {
                _serverProcess = Process.Start(startInfo);
                UnityEngine.Debug.Log($"Started local relay server using {scriptName}. Waiting for port 23144...");

                if (!WaitForPort(23144, 5000))
                {
                    UnityEngine.Debug.LogError("Relay Server failed to open port 23144 within timeout.");
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to start Relay Server: {e.Message}");
            }
        }

        private static bool WaitForPort(int port, int timeoutMs)
        {
            var startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                try
                {
                    using (var client = new TcpClient())
                    {
                        if (client.ConnectAsync("127.0.0.1", port).Wait(100))
                            return true;
                    }
                }
                catch { }
                System.Threading.Thread.Sleep(100);
            }
            return false;
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
                        _serverProcess.WaitForExit(5000);
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
