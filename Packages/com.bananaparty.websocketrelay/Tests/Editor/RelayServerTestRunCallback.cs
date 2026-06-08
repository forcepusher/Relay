using System;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.TestRunner;

[assembly: TestRunCallback(typeof(BananaParty.WebSocketRelay.Tests.Editor.RelayServerTestRunCallback))]

namespace BananaParty.WebSocketRelay.Tests.Editor
{
    /// <summary>
    /// Starts the relay server in the Editor before a test run that needs it.
    /// Required for WebGL player tests, which cannot launch local processes themselves.
    /// </summary>
    public class RelayServerTestRunCallback : ITestRunCallback
    {
        private static bool _serverStartedForRun;

        public void RunStarted(ITest testsToRun)
        {
            if (testsToRun.Parent != null)
                return;

            var task = RelayServerLauncher.EnsureRunningAsync();
            if (!task.Wait(TimeSpan.FromSeconds(15)))
            {
                Debug.LogError("RelayServerTestRunCallback: timed out waiting for relay server to start.");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError($"RelayServerTestRunCallback: failed to start relay server: {task.Exception?.GetBaseException().Message}");
                return;
            }

            if (!task.Result)
            {
                Debug.LogError("RelayServerTestRunCallback: relay server is not reachable.");
                return;
            }

            _serverStartedForRun = true;
        }

        public void RunFinished(ITestResult testResults)
        {
            if (testResults.Test.Parent != null || !_serverStartedForRun)
                return;

            _serverStartedForRun = false;

            var task = RelayServerLauncher.StopAsync();
            if (!task.Wait(TimeSpan.FromSeconds(10)) && !task.IsCompleted)
                Debug.LogError("RelayServerTestRunCallback: timed out waiting for relay server to stop.");
        }

        public void TestStarted(ITest test) { }

        public void TestFinished(ITestResult result) { }
    }
}
