using UnityEngine;

public class CheckCollisionOnDragNewNew : CheckCollisionOnDragNew
{
    protected override void UpdateNumberObject(Collider2D other)
    {
        SetEnumPencil enumPencil = other.GetComponent<SetEnumPencil>();
        string pencilColor = enumPencil.enumPencils.ToString();
        if (pencilColor == "Red")
        {
            NumberObject--;
            NumberTxt.text = NumberObject.ToString();
        }
        else if(pencilColor == "Yellow")
        {
            NumberObject -= 2; 
            NumberTxt.text = NumberObject.ToString();
        }
    }
}

