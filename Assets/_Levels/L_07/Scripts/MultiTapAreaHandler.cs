using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VisualFlow;
public class MultiTapAreaHandler : VisualAction
{
    [SerializeField] private TapArea[] tapAreas;

    [SerializeField] private bool isTapAreaCompleted;
    public bool IsTapAreaCompleted => isTapAreaCompleted;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        isTapAreaCompleted = false;
        foreach (var item in tapAreas)
        {
            if (item.Completed)
            {
                isTapAreaCompleted = true;
                break;
            }
        }
        await UniTask.CompletedTask;

    }
}
