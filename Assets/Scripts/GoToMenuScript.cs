using UnityEngine;

public class GoToMenuScript : MonoBehaviour {

    private bool click;

    void OnMouseDown()
    {
        if (!FindObjectOfType<InterfaceScript>().transform.Find("Menu").gameObject.activeSelf)
            click = true;
    }
    void OnMouseUp()
    {
        if (click)
        {
            click = false;
            FindObjectOfType<PauseScript>().Pause();
            FindObjectOfType<InterfaceScript>().transform.Find("Menu").gameObject.SetActive(true);
        }
    }
}
