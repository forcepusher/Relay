using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
#if !UNITY_WEBGL || UNITY_EDITOR
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
#endif

namespace BananaParty.WebSocketRelay.Tests
{
    public static class RelayServerLauncher
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        private static Process _serverProcess;
        private static bool _weOwnProcess;
#endif

        public static Task StartAsync() => EnsureRunningAsync();

        public static Task<bool> EnsureRunningAsync()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return Task.FromResult(true);
#else
            return EnsureRunningInternalAsync();
#endif
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        private static async Task<bool> EnsureRunningInternalAsync()
        {
            if (await IsPortOpenAsync(TestParameters.RelayServerPort))
                return true;

            if (_serverProcess != null && !_serverProcess.HasExited)
                return await WaitForPortAsync(TestParameters.RelayServerPort, 5000);

            string scriptName = Application.platform switch
            {
                RuntimePlatform.WindowsEditor or RuntimePlatform.WindowsPlayer => "LaunchServer-Windows.bat",
                RuntimePlatform.OSXEditor or RuntimePlatform.OSXPlayer => "LaunchServer-MacOS.sh",
                _ => "LaunchServer-Linux.sh"
            };

            string scriptPath = Path.Combine(Environment.CurrentDirectory, "Packages", "com.bananaparty.websocketrelay", "Server", scriptName);

            if (!File.Exists(scriptPath))
            {
                UnityEngine.Debug.LogError($"Relay Server script not found at: {scriptPath}");
                return false;
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
                _weOwnProcess = true;
                UnityEngine.Debug.Log($"Started local relay server using {scriptName}. Waiting for port {TestParameters.RelayServerPort}...");

                if (await WaitForPortAsync(TestParameters.RelayServerPort, 5000))
                    return true;

                UnityEngine.Debug.LogError($"Relay Server failed to open port {TestParameters.RelayServerPort} within timeout.");
                return false;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to start Relay Server: {e.Message}");
                return false;
            }
        }

        private static async Task<bool> IsPortOpenAsync(int port)
        {
            try
            {
                using TcpClient client = new TcpClient();
                Task connectTask = client.ConnectAsync("127.0.0.1", port);
                if (await Task.WhenAny(connectTask, Task.Delay(100)) == connectTask && !connectTask.IsFaulted)
                    return client.Connected;
            }
            catch { }

            return false;
        }

        private static async Task<bool> WaitForPortAsync(int port, int timeoutMs)
        {
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
            {
                if (await IsPortOpenAsync(port))
                    return true;

                await Task.Delay(100);
            }

            return false;
        }
#endif

        public static IEnumerator StartCoroutine()
        {
            Task<bool> task = EnsureRunningAsync();
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;
        }

        public static IEnumerator StopCoroutine()
        {
            Task task = StopAsync();
            while (!task.IsCompleted) yield return null;
            if (task.IsFaulted) throw task.Exception;
        }

        public static Task StopAsync()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return Task.CompletedTask;
#else
            return StopInternalAsync();
#endif
        }

#if !UNITY_WEBGL || UNITY_EDITOR
        private static async Task StopInternalAsync()
        {
            if (!_weOwnProcess || _serverProcess == null || _serverProcess.HasExited)
            {
                _weOwnProcess = false;
                return;
            }

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
                _weOwnProcess = false;
            }
        }
#endif
    }
}
