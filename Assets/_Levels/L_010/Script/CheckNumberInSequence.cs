using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VisualFlow;

public class CheckNumberInSequence : VisualAction
{
    [SerializeField] private VisualAction conditionCompleted;
    [SerializeField] private VisualAction notConditionCompleted;
    [SerializeField] private UpdateNumberOrder updateNumberOrder;
    [SerializeField] private MultiTapAreaHandler multiTapAreaHandler;
    public bool active;
    [Header("Text Number Check")]
    [SerializeField] private TMP_Text[] numberCheck;

    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        this.active = false;
        int order = updateNumberOrder.currentOrder - 1;
        foreach (var number in numberCheck)
        {
            if (number.text == order.ToString())
            {
                this.active = true;
            }
        }

        if (!this.active && multiTapAreaHandler.IsTapAreaCompleted)
        {
            if (notConditionCompleted != null)
            {
                await notConditionCompleted.Execute(cancellationToken);
            }
        }
        else
        {
            if (conditionCompleted != null)
            {
                await conditionCompleted.Execute(cancellationToken);
            }
        }
        await UniTask.CompletedTask;
    }
}
