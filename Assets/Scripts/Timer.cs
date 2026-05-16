using UnityEngine;

public class Timer : MonoBehaviour {

    float time;
    bool paused;
	// Use this for initialization
	void Start () {
        paused = false;
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(!paused)
            time += Time.deltaTime;
        if (time > 4) Destroy(gameObject);
	}
    public void Pause()
    {
        paused = true;
    }
    public void Play()
    {
        paused = false;
    }
}
