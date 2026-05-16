using UnityEngine;

public class ClickScript : MonoBehaviour {

    public bool clickedIs;
    void OnMouseDown()
    {
        clickedIs = true;
    }
    void OnMouseUp()
    {
        clickedIs = false;
    }
}
