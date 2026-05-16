using UnityEngine;

public class Rotating : MonoBehaviour {

    public float rotation;
    public int size0;
    private float w, c, t1, t2;

    void Start()
    {
		if(Screen.width>Screen.height)
			Camera.main.orthographicSize = 6 / (Mathf.Min(Screen.width, Screen.height) / (float)Mathf.Max(Screen.width, Screen.height));
		else
            Camera.main.orthographicSize = 10 / (Mathf.Min(Screen.width, Screen.height) / (float)Mathf.Max(Screen.width, Screen.height));
        w = Camera.main.orthographicSize * Screen.width / Screen.height;
        c = Mathf.Sqrt(Mathf.Pow(Camera.main.orthographicSize*(Screen.width>Screen.height ? Screen.width/Screen.height : 1f), 2f)*4f + Mathf.Pow(w, 2)) / 3.8f;
        FindObjectOfType<BackgroundScript>().transform.localScale = new Vector3(c, c, 1);
        size0 = (int)Mathf.Sqrt(Mathf.Pow(GetComponent<Camera>().orthographicSize, 2) + Mathf.Pow(w, 2)) + 2;
        FindObjectOfType<ShipScript>().size = size0;

        FindObjectOfType<DustScript1>().Start1();
        FindObjectOfType<DustScript2>().Start1();
        FindObjectOfType<DustScript3>().Start1();
        FindObjectOfType<DustScript4>().Start1();
    }

    public void Rotate (float speed) {
        transform.Rotate(0, 0, speed);
    }

    public void Set()
    {
        rotation = transform.rotation.eulerAngles.z;
        t1 = Mathf.Min((FindObjectOfType<ShipScript>().speed - 0.05f) * Camera.main.orthographicSize, Camera.main.orthographicSize-1f);
        if (FindObjectOfType<ShipScript>().rotateCamera)
        {
            t2 = (rotation + 90) / 180 * Mathf.PI;
            transform.position = new Vector3(Mathf.Cos(t2) * t1 + FindObjectOfType<ShipScript>().transform.position.x, Mathf.Sin(t2) * t1 + FindObjectOfType<ShipScript>().transform.position.y, -10);
        }
        else
            transform.position = new Vector3(FindObjectOfType<ShipScript>().transform.position.x, FindObjectOfType<ShipScript>().transform.position.y, -10);
        FindObjectOfType<ShipScript>().size = (int)Mathf.Sqrt(Mathf.Pow(GetComponent<Camera>().orthographicSize, 2) * (t1 + 1) + Mathf.Pow(GetComponent<Camera>().orthographicSize * Screen.width / Screen.height, 2)) + 2;
        FindObjectOfType<BackgroundScript>().transform.position = new Vector3(transform.position.x, transform.position.y,0);
    }

}