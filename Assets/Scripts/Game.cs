using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public static AudioClip[] destroyingAsteroid, pickingEnergy;
    public AudioClip[] clip1, clip2;
    public AudioClip clip3, clip4, clip5;
    public static AudioClip menuButton, music, destroyingShip;
    public static AudioSource soundsSource, musicSource;
    // Use this for initialization
    void Start () {
        soundsSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        destroyingAsteroid = clip1;
        pickingEnergy = clip2;
        menuButton = clip3;
        music = clip4;
        destroyingShip = clip5;
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.Play();
    }

}
