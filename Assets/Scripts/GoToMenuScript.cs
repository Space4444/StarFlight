using UnityEngine;

public class GoToMenuScript : MonoBehaviour {

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
            FindObjectOfType<PauseScript>().Pause();
            FindObjectOfType<InterfaceScript>().transform.FindChild("Menu").gameObject.SetActive(true);
        }
    }
}
