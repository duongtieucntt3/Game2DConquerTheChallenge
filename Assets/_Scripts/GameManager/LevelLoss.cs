using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VisualFlow;

public class LevelLoss : VisualAction
{
    private AddressableSampleArray addressableSample;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        if (HealthManager.health > 1)
        {
            HealthManager.health--;
            addressableSample.OnPlayerLose();
        }
        else
        {
            GameManager.Instance.SetGameLose();
            addressableSample.UnLevels();
        }
        await UniTask.CompletedTask;
    }
    private void Awake()
    {
        LoadAddressableSampleArray();
    }

    protected virtual void LoadAddressableSampleArray()
    {
        if (this.addressableSample != null) return;
        GameObject go = GameObject.Find("Main Camera");
        this.addressableSample = go.GetComponent<AddressableSampleArray>();
    }


}