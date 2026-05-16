using UnityEngine;

public class Switch2Script : MonoBehaviour {

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
            gameObject.GetComponent<SpriteRenderer>().enabled = !gameObject.GetComponent<SpriteRenderer>().enabled;
        }
    }
}
