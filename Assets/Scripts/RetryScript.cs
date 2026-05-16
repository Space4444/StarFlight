using UnityEngine;

public class RetryScript : MonoBehaviour {

    private bool click;

    void OnMouseDown()
    {
        if (!FindObjectOfType<InterfaceScript>().transform.FindChild("Menu").gameObject.activeSelf)
            click = true;
    }
    void OnMouseUp()
    {
        if (click)
        {
            click = false;
            FindObjectOfType<PauseScript>().Play();
            FindObjectOfType<ShipScript>().NewGame();
        }
    }
}
