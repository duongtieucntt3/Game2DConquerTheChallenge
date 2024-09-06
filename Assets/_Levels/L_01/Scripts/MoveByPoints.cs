using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VisualFlow;

public class MoveByPoints : VisualAction
{
    [SerializeField] private VisualAction onEnableCollider;
    [SerializeField] private float duration = 10f;
    [SerializeField] private Ease ease;
    public List<Transform> points;
    [SerializeField] private bool speedBase = false;
    private Tween currentTween;
    private bool isCollided = false;
    protected override async UniTask OnExecuting(CancellationToken cancellationToken)
    {
        isCollided = false;
        foreach (var point in points)
        {
            await this.MoveFollowPoint(point.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            isCollided = true;
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Kill();
            }
        }

    }
    protected virtual async UniTask MoveFollowPoint(Vector3 pointPosition)
    {
        if (isCollided) return;
        currentTween = transform.DOMove(pointPosition, duration).SetEase(ease)
            .SetSpeedBased(speedBase);
        await currentTween.AsyncWaitForCompletion();
        if (isCollided)
        {
            if (onEnableCollider != null)
            {
                onEnableCollider.Execute(CancellationToken.None);
            }
            
        }
    }
}