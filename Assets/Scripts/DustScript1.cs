using UnityEngine;

public class DustScript1 : MonoBehaviour {

    public float c;
    // Use this for initialization
    public void Start1 ()
    {
        float w = Camera.main.orthographicSize * Screen.width / Screen.height;
        c = Mathf.Sqrt(Mathf.Pow(w, 2) + Mathf.Pow(Camera.main.orthographicSize, 2)) * 4;
        transform.localScale = new Vector3(c / 4, c / 4, 1);
        transform.position = new Vector3(FindObjectOfType<ShipScript>().transform.position.x, FindObjectOfType<ShipScript>().transform.position.y, 0);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Round(FindObjectOfType<ShipScript>().transform.position.x / c) * c, Mathf.Round(FindObjectOfType<ShipScript>().transform.position.y / c) * c, 0);
	}
}
