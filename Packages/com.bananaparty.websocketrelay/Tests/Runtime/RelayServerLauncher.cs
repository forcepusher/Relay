using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

namespace BananaParty.WebSocketRelay.Tests
{
    public static class RelayServerLauncher
    {
        private static Process _serverProcess;

        public static async Task StartAsync()
        {
            if (_serverProcess != null && !_serverProcess.HasExited) return;

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                UnityEngine.Debug.LogWarning("RelayServerLauncher: Cannot start server from WebGL. Assuming server is already running.");
                return;
            }

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

                if (!await WaitForPortAsync(23144, 5000))
                {
                    UnityEngine.Debug.LogError("Relay Server failed to open port 23144 within timeout.");
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to start Relay Server: {e.Message}");
            }
        }

        private static async Task<bool> WaitForPortAsync(int port, int timeoutMs)
        {
            var startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                try
                {
                    using (var client = new TcpClient())
                    {
                        var connectTask = client.ConnectAsync("127.0.0.1", port);
                        if (await Task.WhenAny(connectTask, Task.Delay(100)) == connectTask)
                            return true;
                    }
                }
                catch { }
                await Task.Delay(100);
            }
            return false;
        }

        public static IEnumerator StartCoroutine()
        {
            var task = StartAsync();
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;
        }

        public static IEnumerator StopCoroutine()
        {
            var task = StopAsync();
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;
        }

        public static async Task StopAsync()
        {
            if (_serverProcess == null || _serverProcess.HasExited) return;

            try
            {
                _serverProcess.Kill();
                await Task.Run(() => _serverProcess.WaitForExit(5000));
                _serverProcess.Dispose();
                UnityEngine.Debug.Log("Stopped local relay server.");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to stop Relay Server: {e.Message}");
            }
            finally
            {
                _serverProcess = null;
            }
        }
    }
}
