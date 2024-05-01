using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    void Start() {
        GetComponent<AudioSource>().volume = .1f;
        PlayMusic();
    }

    void PlayMusic() {
        GetComponent<AudioSource>().Play();
    }

    void StopMusic() {
        GetComponent<AudioSource>().Stop();
    }
}
