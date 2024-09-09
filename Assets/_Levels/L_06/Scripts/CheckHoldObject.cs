using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using VisualFlow;

public class CheckHoldObject : VisualAction
{
    [SerializeField] private Transform objectHold;
    [SerializeField] private VisualAction timerAction;
    [SerializeField] private VisualAction winAction;
    [SerializeField] private VisualAction loseAction;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        await UniTask.WaitUntil(() => this.timerAction.Completed || CompleteTrigger, cancellationToken: cancellationToken);
        if (this.timerAction.Completed)
        {
            PauseObjectHold();
            await this.winAction.Execute(cancellationToken);
        }
        else
        {
            await this.loseAction.Execute(cancellationToken);
        }
    }
    protected virtual void PauseObjectHold()
    {
        Rigidbody2D rb2d = objectHold.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.gravityScale = 0f;
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
        }
    }
    public bool CompleteTrigger { private set; get; }

    public void SetCompleteTrigger(bool complete)
    {
        CompleteTrigger = complete;
    }
}
