using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;
using VisualFlow;

public class CheckNumber : VisualAction
{
    [SerializeField] private VisualAction conditionCompleted;
    [SerializeField] private VisualAction notConditionCompleted;
    [SerializeField] private int number;
    [SerializeField] private TMP_Text textCheck;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        if (textCheck.text == number.ToString())
        {
            if (conditionCompleted != null)
            {
                await conditionCompleted.Execute(cancellationToken);
            }
        }
        else
        {
            if (notConditionCompleted != null)
            {
                await notConditionCompleted.Execute(cancellationToken);
            }
        }
        await UniTask.CompletedTask;
    }
}

