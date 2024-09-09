using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using VisualFlow;
using UnityEngine.SceneManagement;

public class LevelReplay : VisualAction
{
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}