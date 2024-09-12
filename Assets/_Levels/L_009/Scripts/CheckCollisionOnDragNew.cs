using System.Threading;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using VisualFlow;

public class CheckCollisionOnDragNew : MonoBehaviour
{
    [SerializeField] private VisualAction notDestinationReached;
    [SerializeField] private VisualAction destinationReached;

    [SerializeField] private int numberObject;
    public int NumberObject { get => numberObject; set => numberObject = value; }

    [SerializeField] private TMP_Text numberTxt;
    public TMP_Text NumberTxt { get => numberTxt; set => numberTxt = value; }

    private bool isCollided;
    public bool IsCollided { get => isCollided; set => isCollided = value; }

    private enum CollisionType
    {
        Enemy,
        Boom,
        Goal,
        None
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionType collisionType = GetCollisionType(collision);
        switch (collisionType)
        {
            case CollisionType.Enemy:
                HandleEnemyCollision(collision);
                break;

            case CollisionType.Boom:
                HandleBoomCollision(collision);
                break;
            case CollisionType.Goal:
                HandleGoalCollision(collision);
                break;
        }

    }
    private CollisionType GetCollisionType(Collider2D collider)
    {
        if (collider.CompareTag("Enemy")) return CollisionType.Enemy;
        if (collider.CompareTag("Boom")) return CollisionType.Boom;
        if (collider.CompareTag("Goal")) return CollisionType.Goal;
        return CollisionType.None;
    }
    protected virtual void HandleEnemyCollision(Collider2D other)
    {
        this.isCollided = true;
        ExecuteAction(this.notDestinationReached);
    }
    protected virtual void HandleBoomCollision(Collider2D other)
    {
        this.isCollided = true;
        Destroy(other.gameObject);
        ExecuteAction(this.notDestinationReached);
    }
    protected virtual void HandleGoalCollision(Collider2D other)
    {

        Destroy(other.gameObject);
        this.UpdateNumberObject(other);
        this.CheckNumber();
    }
    protected virtual void ExecuteAction(VisualAction action)
    {
        action?.Execute(CancellationToken.None);
    }
    protected virtual void UpdateNumberObject(Collider2D other)
    {
        numberObject--;
        numberTxt.text = numberObject.ToString();
    }
    protected virtual void CheckNumber()
    {
        if (numberObject == 0)
        {
            this.isCollided = true;
            ExecuteAction(this.destinationReached);
        }
    }
}



