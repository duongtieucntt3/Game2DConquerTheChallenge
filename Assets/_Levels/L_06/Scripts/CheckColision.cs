using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VisualFlow;

public class CheckColision : VisualAction
{
    [SerializeField] private bool isCollided;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        isCollided = false;
        await UniTask.WaitUntil(() => this.isCollided || CompleteTrigger, cancellationToken: cancellationToken);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            isCollided = true;
        }
    }
    public bool CompleteTrigger { private set; get; }

    public void SetCompleteTrigger(bool complete)
    {
        CompleteTrigger = complete;
    }
}
