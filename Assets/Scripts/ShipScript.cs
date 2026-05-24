using UnityEngine;
using UnityEngine.UI;

public class ShipScript : MonoBehaviour {

    public bool rotateCamera, accelerometer, isRotate;
    public int size, n;
    public float speed, health, startSpeed;
    public GameObject heartShield, asteroid, asteroidExplodion, explodion, energy, hp;
    public ParticleSystem track;
    public GameObject[] explodions;
    public ClickScript control;
    public ShieldScript shield;
    public PauseScript pause;
    public InterfaceScript interfaceScript;
    public Rotating camRotating;
    private bool hit, isProtected;
    private int record;
    private float maxHPHeigt, t, timer, sp, score, rotSpeed, dx, dy, delta, tempVar, angle, cos, sin, a, b;
    private GameObject[] asteroids;
    public RectTransform emptyHp;
    private RectTransform hpRect;
    void Awake()
    {
        Application.targetFrameRate = 24;
    }
    // Use this for initialization
    void Start() {
        rotateCamera = true;
        accelerometer = false;
        hit = false;
        isProtected = false;
        n = 12;
        timer = 0;
        score = 0;
        rotSpeed = 4;
        if (PlayerPrefs.HasKey("Record"))
        {
            record = PlayerPrefs.GetInt("Record");
            interfaceScript.transform.Find("Text (1)").GetComponent<Text>().text = "record: " + record.ToString();
        }
        else
            record = 0;
        health = 10f;
        sp = 0;
        //startSpeed = 0.3f;
        explodions = new GameObject[n];
        asteroids = new GameObject[n];
        shield.unProtect();
        for (int i = 0; i < n; i++)
        {
            asteroids[i] = Instantiate(asteroid);
            randomizeCoords(asteroids[i]);
            asteroids[i].GetComponent<AsteroidScript>().rotateSpeed = Random.Range(-4, 4);
            float scale = (float)Random.Range(10, 50)/10;
            asteroids[i].transform.localScale = new Vector3(scale, scale, 1);
        }
        hpRect = hp.GetComponent<RectTransform>();
        maxHPHeigt = emptyHp.rect.height;
        energy = Instantiate(energy);
        energy.SetActive(false);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    public float dr;
    // Update is called once per frame
    void FixedUpdate ()
    {
        maxHPHeigt = emptyHp.rect.height;
        hpRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 80f, maxHPHeigt * health * 0.1f);
        if (!pause.paused)
        {
            health -= 0.01f*speed;
            if (score >= record)
            {
                record = (int)score;
                if (score >= record) PlayerPrefs.SetInt("Record", record);
                interfaceScript.transform.Find("Text (1)").GetComponent<Text>().text = "record: " + record.ToString();
            }
            if (health <= 0)
            {
                if (!track.isStopped)
                    track.Stop();
                timer += Time.deltaTime;
                speed *= 0.95f;
                if (timer > 3f)
                {
                    timer = 0f;
                    NewGame();
                }
            }
            else
            {
                sp += 0.000001f * t;
				score += sp*1000*speed;
                speed = 5f - (0.5f-startSpeed*0.1f) / (sp + 0.1f);
                rotSpeed = 4f + speed * 4f;
                interfaceScript.transform.Find("Text").GetComponent<Text>().text = "score: " + ((int)score).ToString();
                if (!energy.activeSelf && Random.Range(0, 10) == 4)
                {
                    energy.SetActive(true);
                    randomizeCoords(energy);
                }
            }

            t = 40 * Time.deltaTime;

            if (rotateCamera)
            {
                dr = transform.rotation.eulerAngles.z - camRotating.rotation - 90;
                if (dr > 180) dr -= 360;
                else if (dr < -180) dr += 360;
                camRotating.Rotate(dr * t / 7);
            }
            else camRotating.Rotate(-Camera.main.transform.eulerAngles.z / 10);

            hit = false;

            if (health >= 0)
            {
                if (accelerometer)
                {
                    dx = Input.acceleration.x + Screen.width / 2;
                    dy = Input.acceleration.y + Screen.height / 2;
                }
                else
                {
                    if (control.clickedIs) isRotate = true; else isRotate = false;
                    dx = Input.mousePosition.x;
                    dy = Input.mousePosition.y;
                }
                if (isRotate)
                    if (rotateCamera)
                    {
                        if(accelerometer)
                            transform.Rotate(0, 0, Mathf.Sign(Screen.width / 2 - dx) * rotSpeed * Mathf.Min(Mathf.Abs(dx - Screen.width / 2) * 2, 1f) * t);
                        else
                            transform.Rotate(0, 0, Mathf.Sign(Screen.width / 2 - dx) * rotSpeed * t);
                    }
                    else
                    {
                        delta = Mathf.Atan2(dy - Screen.height / 2, dx - Screen.width / 2) * Mathf.Rad2Deg - transform.eulerAngles.z;
                        if (delta > 180f) delta -= 360f;
                        else if (delta < -180f) delta += 360f;
                        tempVar = rotSpeed * t;
                        if (Mathf.Abs(delta) > tempVar)
                        {
                            if (delta > 0)
                                transform.Rotate(0, 0, tempVar);
                            else
                                transform.Rotate(0, 0, -tempVar);
                        }
                        else
                            transform.Rotate(0, 0, delta);
                    }
            }

            angle = transform.eulerAngles.z * Mathf.Deg2Rad;
            cos = Mathf.Cos(angle);
            sin = Mathf.Sin(angle);
            transform.position = new Vector3(transform.position.x + speed * cos * t, transform.position.y + speed * sin * t, 0);
            camRotating.Set();
            for (int i = 0; i < n; i++)
            {
                asteroids[i].transform.Rotate(0, 0, asteroids[i].GetComponent<AsteroidScript>().rotateSpeed * t);
                if (health >= 0)
                {
                    if (asteroids[i].transform.position.x > size + transform.position.x + 4)
                    {
                        randomizeCoords(asteroids[i]);
                        asteroids[i].GetComponent<AsteroidScript>().rotateSpeed = Random.Range(-4, 4);
                        float scale = (float)Random.Range(10, 50) / 10;
                        asteroids[i].transform.localScale = new Vector3(scale, scale, 1);
                    }
                    if (asteroids[i].transform.position.x < -size + transform.position.x - 4)
                    {
                        randomizeCoords(asteroids[i]);
                        asteroids[i].GetComponent<AsteroidScript>().rotateSpeed = Random.Range(-4, 4);
                        float scale = (float)Random.Range(10, 50) / 10;
                        asteroids[i].transform.localScale = new Vector3(scale, scale, 1);
                    }
                    if (asteroids[i].transform.position.y > size + transform.position.y + 4)
                    {
                        randomizeCoords(asteroids[i]);
                        asteroids[i].GetComponent<AsteroidScript>().rotateSpeed = Random.Range(-4, 4);
                        float scale = (float)Random.Range(10, 50) / 10;
                        asteroids[i].transform.localScale = new Vector3(scale, scale, 1);
                    }
                    if (asteroids[i].transform.position.y < -size + transform.position.y - 4)
                    {
                        randomizeCoords(asteroids[i]);
                        asteroids[i].GetComponent<AsteroidScript>().rotateSpeed = Random.Range(-4, 4);
                        float scale = (float)Random.Range(10, 50) / 10;
                        asteroids[i].transform.localScale = new Vector3(scale, scale, 1);
                    }
                }
                a = asteroids[i].transform.position.x - transform.position.x - cos / 2;
                b = asteroids[i].transform.position.y - transform.position.y - sin / 2;
                if (Mathf.Sqrt(a * a + b * b) < 1 + asteroids[i].transform.localScale.x && health >= 0)
                {
                    explodions[i] = (GameObject)Instantiate(asteroidExplodion, new Vector3(asteroids[i].transform.position.x, asteroids[i].transform.position.y, 0), Quaternion.identity);
                    explodions[i].GetComponent<ParticleSystem>().startSize = asteroids[i].transform.localScale.x * 2.5f;
                    randomizeCoords(asteroids[i]);
                    asteroids[i].GetComponent<AsteroidScript>().rotateSpeed = Random.Range(-4, 4);
                    if (!isProtected)
                        hit = true;
                    Game.soundsSource.PlayOneShot(Game.destroyingAsteroid[Random.Range(0, Game.destroyingAsteroid.Length - 1)]);
                }
            }
            if (energy.activeSelf)
            {
                if (Mathf.Abs(energy.transform.position.x - transform.position.x) > size || Mathf.Abs(energy.transform.position.y - transform.position.y) > size)
                    energy.SetActive(false);
                a = energy.transform.position.x - transform.position.x - cos / 2;
                b = energy.transform.position.y - transform.position.y - sin / 2;
                if (Mathf.Sqrt(a * a + b * b) < 2 && health >= 0 && health < 10)
                {
                    Game.soundsSource.PlayOneShot(Game.pickingEnergy[Random.Range(0, Game.pickingEnergy.Length - 1)]);
                    energy.SetActive(false);
                    health++;
                }
            }
            if (isProtected)
            {
                timer += Time.deltaTime;
                if (timer > 2)
                {
                    timer = 0;
                    shield.unProtect();
                    isProtected = false;
                }
            }
            if (hit)
            {
                if (health > 1)
                {
                    health--;
                    shield.Protect();
                    isProtected = true;
                }
                else
                {
                    health--;
                    GetComponent<SpriteRenderer>().enabled = false;
                    transform.Find("Particle System").GetComponent<ParticleSystem>().Stop();
                    Instantiate(explodion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    Game.soundsSource.PlayOneShot(Game.destroyingShip);
                }
            }
        }
    }
    private void randomizeCoords(GameObject g)
    {
        if (Random.value > 0.5f)
            g.transform.position = new Vector3((Random.Range(0, 2) * 2 - 1) * Random.Range(size, size + 4) + transform.position.x, Random.Range(-size, size) + transform.position.y, 0);
        else
            g.transform.position = new Vector3(Random.Range(-size, size) + transform.position.x, (Random.Range(0, 2) * 2 - 1) * Random.Range(size, size + 4) + transform.position.y, 0);
    }
    public void NewGame()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = new Vector3(0,0,0);
        sp = 0;
        health = 10;
        score = 0;
        transform.Find("Particle System").GetComponent<ParticleSystem>().Play();
        shield.transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < n; i++)
        {
            randomizeCoords(asteroids[i]);
            asteroids[i].GetComponent<AsteroidScript>().rotateSpeed = Random.Range(-4, 4);
        }
    }
    public void Pause()
    {
        transform.Find("Particle System").GetComponent<ParticleSystem>().Pause();
        if (energy.activeSelf) energy.GetComponent<ParticleSystem>().Pause();
        for (int i = 0; i < n; i++)
            if (explodions[i] != null)
            {
                explodions[i].GetComponent<ParticleSystem>().Pause();
                explodions[i].GetComponent<Timer>().Pause();
            }
    }
    public void Play()
    {
        transform.Find("Particle System").GetComponent<ParticleSystem>().Play();
        if (energy.activeSelf) energy.GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < n; i++)
            if (explodions[i] != null)
            {
                explodions[i].GetComponent<ParticleSystem>().Play();
                explodions[i].GetComponent<Timer>().Play();
            }
    }
}
