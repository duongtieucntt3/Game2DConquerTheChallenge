using System.Collections.Generic;
using NaughtyAttributes;
using Lean.Common;
using Lean.Touch;
using UnityEngine;

public class DragLockAxisWithFixedRange : MonoBehaviour
{
    [SerializeField, Required]
    private Transform targetTransform;
    [SerializeField, Required]
    private LeanSelectable selectable;
        
    [SerializeField] private float startDragZone;
    [SerializeField] private float endDragZone;
    
    
    private void OnEnable()
    {
        LeanTouch.OnFingerUpdate += FingerUpdateHandler;
        LeanTouch.OnFingerUp += FingerUpHandler;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerUpdate -= FingerUpdateHandler;
        LeanTouch.OnFingerUp -= FingerUpHandler;
    }

    private void FingerUpdateHandler(LeanFinger finger)
    {
        if (!this.selectable.IsSelected) return;
        var position = this.targetTransform.position;
        var newPosition = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
        position = new Vector3(Mathf.Clamp(newPosition.x, this.startDragZone, this.endDragZone), position.y);
        this.targetTransform.position = position;
    }

    private void FingerUpHandler(LeanFinger finger)
    {
        if (!this.selectable.IsSelected) return;
        this.selectable.Deselect();

    }

}
