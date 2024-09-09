using TMPro;
using UnityEngine;
using VisualFlow;

public class ChangeTextAfterFinish : MonoBehaviour
{
    [SerializeField] private VisualAction visualAction;
    [SerializeField] private UpdateNumberOrder updateNumberOrder;
    [SerializeField] private TMP_Text numberTxt;

    private bool isChecking = true;

    private void Update()
    {
        if (this.visualAction.Completed && this.isChecking)
        {
            this.isChecking = false;
            this.numberTxt.text = this.updateNumberOrder.currentOrder.ToString();
            this.updateNumberOrder.UpdateNumericalOrder();
        }
    }
}
