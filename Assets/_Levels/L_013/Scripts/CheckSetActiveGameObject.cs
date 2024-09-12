using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VisualFlow;

public class CheckSetActiveGameObject : VisualAction
{
    [SerializeField] private  SetActiveToggleGameObjectState ObjectToggle;
    [SerializeField] private VisualAction notCompleteCondition;
    [SerializeField] private VisualAction completeCondition;
 
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        if (ObjectToggle.Active == true)
        {
            if (completeCondition != null)
            {
                await completeCondition.Execute(cancellationToken);
            }
        }
        else 
        {
            if (notCompleteCondition != null)
            {
                await notCompleteCondition.Execute(cancellationToken);
            }


        }
        await UniTask.CompletedTask;

    }
}

