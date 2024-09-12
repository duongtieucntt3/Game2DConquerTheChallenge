using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VisualFlow;

public class SetActiveToggleGameObjectState : VisualAction
{
    [SerializeField] private GameObject go; 
    protected bool active;
    public bool Active => active;

    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        this.active = false;
        if(go.activeSelf)
        {
            this.go.SetActive(false);
            active = false;
        }
        else
        {
            this.go.SetActive(true);
            active = true;
        }
        await UniTask.CompletedTask;
    }


}
