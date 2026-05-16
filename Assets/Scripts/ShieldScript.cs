using UnityEngine;

public class ShieldScript : MonoBehaviour {

    private bool p;
    private float s;

    void Start()
    {
        s = 0;
        p = false;
        transform.localScale = new Vector3(0, 0, 0);
    }


	public void Protect () {
        p = true;
	}
	
	// Update is called once per frame
	public void unProtect() {
        p = false;
    }

    void Update()
    {
        if(p && s<1)
        {
            s += Time.deltaTime*3;
            transform.localScale = new Vector3(s, s, 1);
        }
        if (!p && s > 0)
        {
            s -= Mathf.Min(Time.deltaTime*3, s);
            transform.localScale = new Vector3(s, s, 1);
        }
    }
}
