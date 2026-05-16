using UnityEngine;

public class BackScript : MonoBehaviour {

    private bool click;

    void OnMouseDown()
    {
        click = true;
    }
    void OnMouseUp()
    {
        if (click)
        {
            click = false;
            transform.parent.gameObject.SetActive(false);
        }
    }
}
