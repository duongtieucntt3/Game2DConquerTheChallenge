using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;
using VisualFlow;

public class SpawnObjectRandomPoints : VisualAction
{
    [SerializeField] private Transform holder;
    [SerializeField] private Transform target;
    [SerializeField] private CheckCollisionOnDragNew checkCollisionOnDrag;
    [SerializeField] private RandomPoints randomPoints;
    [SerializeField] private float duration = 1;
    [SerializeField] private float randomDelay = 1f;
    [SerializeField] private float randomTimer = 0f;
    [SerializeField] private float precentRandom;
    private bool active = false;
    private Tween movement;
    [Header("Object Spawn")]
    [SerializeField] private List<GameObject> objects;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        this.checkCollisionOnDrag.IsCollided = false;
        this.active = true;
        await UniTask.WaitUntil(() => checkCollisionOnDrag.IsCollided, cancellationToken: cancellationToken);
    }
    private void FixedUpdate()
    {
        if (this.active)
        {
            this.Spawning();
        }

    }
    protected virtual void Spawning()
    {
        if (checkCollisionOnDrag.IsCollided)
        {
            if (movement != null && movement.IsActive())
            {
                movement.Kill();
            }
            return;
        }
        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform ranPoint = this.randomPoints.GetRandom();

        this.MoveObject(ranPoint);

    }
    protected virtual void MoveObject(Transform ranPoint)
    {
        Vector3 pos = ranPoint.position;
        Quaternion rot = transform.rotation;
        GameObject obj = InstantiateRandomGameObject(pos, rot);
        obj.transform.SetParent(this.holder);
        obj.gameObject.SetActive(true);
        movement = obj.transform.DOMoveY(target.position.y, duration).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(obj.gameObject);
            });
    } 
    protected virtual GameObject GetRandomObject()
    {
        float randomValue = Random.Range(0f, 1f);
        
        if (randomValue < this.precentRandom)
        {
            return objects[0]; 
        }
        else 
        {
           int rand= Random.Range(1, this.objects.Count);
            return this.objects[rand]; 
        }

    }
    protected virtual GameObject InstantiateRandomGameObject(Vector3 position, Quaternion rotation)
    {
        GameObject randomObject = GetRandomObject();
        return Instantiate(randomObject, position, rotation);
    }


}
