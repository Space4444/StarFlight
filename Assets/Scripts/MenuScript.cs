using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private ShipScript script;
    private Transform settings;
    void Start()
    {
        script = FindObjectOfType<ShipScript>();
        settings = transform.FindChild("settings page");
    }
    public void Play()
    {
        FindObjectOfType<PauseScript>().Play();
        gameObject.SetActive(false);
        Game.soundsSource.PlayOneShot(Game.menuButton);
    }
    public void Settings()
    {
        settings.gameObject.SetActive(true);
        Game.soundsSource.PlayOneShot(Game.menuButton);
    }
    public void Credits()
    {
        transform.FindChild("credits page").gameObject.SetActive(true);
        Game.soundsSource.PlayOneShot(Game.menuButton);
    }
    public void Back()
    {
        transform.FindChild("settings page").gameObject.SetActive(false);
        Game.soundsSource.PlayOneShot(Game.menuButton);
    }
    public void Back2()
    {
        transform.FindChild("credits page").gameObject.SetActive(false);
        Game.soundsSource.PlayOneShot(Game.menuButton);
    }
    public void Accelerometer()
    {
        Game.soundsSource.PlayOneShot(Game.menuButton);
        script.accelerometer = !FindObjectOfType<ShipScript>().accelerometer;
        script.isRotate = FindObjectOfType<ShipScript>().accelerometer;
    }
    public void RotateCamera()
    {
        Game.soundsSource.PlayOneShot(Game.menuButton);
        script.rotateCamera = !FindObjectOfType<ShipScript>().rotateCamera;
    }
    public void Music()
    {
        Game.soundsSource.PlayOneShot(Game.menuButton);
        Game.musicSource.enabled = !Game.musicSource.enabled;
        settings.FindChild("Slider").gameObject.SetActive(!settings.FindChild("Slider").gameObject.activeSelf);
    }
    public void Sound()
    {
        Game.soundsSource.enabled = !Game.soundsSource.enabled;
        settings.FindChild("Slider (1)").gameObject.SetActive(!settings.FindChild("Slider (1)").gameObject.activeSelf);
        Game.soundsSource.PlayOneShot(Game.menuButton);
    }
    public void MusicVolume(float f)
    {
        Game.musicSource.volume = f;
    }
    public void SoundVolume(float f)
    {
        Game.soundsSource.volume = f;
    }
    public void Difficulty(float f)
    {
        script.startSpeed = f + 0.05f;
        script.NewGame();
    }
}