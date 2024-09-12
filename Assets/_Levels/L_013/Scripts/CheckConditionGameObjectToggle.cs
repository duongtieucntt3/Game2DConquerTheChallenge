using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VisualFlow;

public class CheckConditionGameObjectToggle : VisualAction
{
    [SerializeField] private VisualAction notCompleteAction;
    [SerializeField] private VisualAction completeAction;
    [SerializeField] private SetActiveToggleGameObjectState[] objectToggles;
    private int activeCount = 0;
    [SerializeField] private int SumLightOn;
    
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        activeCount = 0;
        foreach (var item in objectToggles)
        {
            if(item.Active)
            {
                activeCount++;
            }            
        }      
        if (activeCount == SumLightOn )
        {
            if (completeAction != null)
            {
                await completeAction.Execute(cancellationToken);
            }
        }
        else
        {
            if (notCompleteAction != null)
            {
                await notCompleteAction.Execute(cancellationToken);
            }
            activeCount = 0;
        }
        await UniTask.CompletedTask;

    }
}
