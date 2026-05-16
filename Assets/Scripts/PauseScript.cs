using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

    public bool paused;
    public Sprite pauseSprite, playSprite;
    private bool click;

    // Use this for initialization
	void Start () {
        paused = true;
        FindObjectOfType<ShipScript>().transform.FindChild("Particle System").GetComponent<ParticleSystem>().Pause();
    }
	void OnMouseDown()
    {
		if(!FindObjectOfType<InterfaceScript>().transform.FindChild("Menu").gameObject.activeSelf)
            click = true;
    }
    void OnMouseUp()
    {
        if (click)
        {
            if (paused)
                Play();
            else Pause();
        }
        click = false;
    }
    void OnApplicationFocus()
    {
        Pause();
    }
    public void Pause()
    {
        paused = true;
        GetComponent<Image>().sprite = playSprite;
        FindObjectOfType<ShipScript>().Pause();
    }
    public void Play()
    {
        paused = false;
        GetComponent<Image>().sprite = pauseSprite;
        FindObjectOfType<ShipScript>().Play();
    }
    void Update()
    {
        if(paused)
            transform.parent.FindChild("buttons").localPosition = new Vector3(transform.parent.FindChild("buttons").localPosition.x - (transform.parent.FindChild("buttons").localPosition.x) / 10, transform.parent.FindChild("buttons").localPosition.y, 0);
        else
            transform.parent.FindChild("buttons").localPosition = new Vector3(transform.parent.FindChild("buttons").localPosition.x - (transform.parent.FindChild("buttons").localPosition.x - 111) / 10, transform.parent.FindChild("buttons").localPosition.y, 0);
    }
}
