using UnityEngine;
using VisualFlow;

public class UpdateNumberOrder : MonoBehaviour
{
    public int currentOrder = 1;

    public void UpdateNumericalOrder()
    {
        this.currentOrder += 1;
    }
}
