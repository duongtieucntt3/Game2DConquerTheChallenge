using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VisualFlow;

public class TapAreaStatusChecker : VisualAction
{    
    [SerializeField] private VisualAction executedAction;
    [SerializeField] private TapArea TapAreaCheck;
    [SerializeField] private MultiTapAreaHandler  multiTapAreaHandler;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        if (!TapAreaCheck.Completed && this.multiTapAreaHandler.IsTapAreaCompleted)  
        {
            if (this.executedAction != null)
            {
                await this.executedAction.Execute(cancellationToken);
            }
        }

    }
}
