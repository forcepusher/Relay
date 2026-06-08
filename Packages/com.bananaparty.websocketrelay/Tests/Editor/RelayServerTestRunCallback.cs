using System.Threading.Tasks;
using BananaParty.WebSocketRelay.Tests;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.TestRunner;

[assembly: TestRunCallback(typeof(BananaParty.WebSocketRelay.Tests.Editor.RelayServerTestRunCallback))]

namespace BananaParty.WebSocketRelay.Tests.Editor
{
    /// <summary>
    /// Starts the relay server when a test run begins in the Editor.
    /// Uses fire-and-forget async so the main thread is never blocked on Task.Wait.
    /// Tests that need the server also wait in UnitySetUp via RelayServerLauncher.StartCoroutine().
    /// </summary>
    public class RelayServerTestRunCallback : ITestRunCallback
    {
        private static bool _serverStartedForRun;

        public void RunStarted(ITest testsToRun)
        {
            if (testsToRun.Parent != null)
                return;

            _serverStartedForRun = true;
            RelayServerLauncher.EnsureRunningAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    Debug.LogError($"RelayServerTestRunCallback: failed to start relay server: {task.Exception?.GetBaseException().Message}");
                else if (!task.Result)
                    Debug.LogError("RelayServerTestRunCallback: relay server is not reachable.");
            }, TaskScheduler.Default);
        }

        public void RunFinished(ITestResult testResults)
        {
            if (testResults.Test.Parent != null || !_serverStartedForRun)
                return;

            _serverStartedForRun = false;
            RelayServerLauncher.StopAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    Debug.LogError($"RelayServerTestRunCallback: failed to stop relay server: {task.Exception?.GetBaseException().Message}");
            }, TaskScheduler.Default);
        }

        public void TestStarted(ITest test) { }

        public void TestFinished(ITestResult result) { }
    }
}
