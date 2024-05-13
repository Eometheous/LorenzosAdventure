using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public AudioClip mainMenu, victory, tower;
    bool isMainMenuPlaying, isVictoryPlaying, isTowerPlaying;

    int currentLevel;

    void Awake() {
        isMainMenuPlaying = true;
        currentLevel =  SceneManager.GetActiveScene().buildIndex;
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

    void FixedUpdate()
    {
        
        int level = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel != level) 
        {
            if (level < 2 && !isMainMenuPlaying) 
            {
                ChangeMusic(mainMenu);
                isMainMenuPlaying = true;
                isTowerPlaying = false;
                isVictoryPlaying = false;
            }
            else if (level >= 2 && level < 13 && !isTowerPlaying) 
            {
                ChangeMusic(tower);
                isTowerPlaying = true;
                isMainMenuPlaying = false;
                isVictoryPlaying = false;
            }
            else if (level == 13 && !isVictoryPlaying)
            {
                ChangeMusic(victory);
                isVictoryPlaying = true;
                isMainMenuPlaying = false;
                isTowerPlaying = false;
            }
            currentLevel = level;
        }
    }

    void ChangeMusic(AudioClip newClip) 
    {
        GetComponent<AudioSource>().clip = newClip;
        if (GetComponent<AudioSource>().isPlaying) StopMusic();
        else PlayMusic();
    }

    void PlayMusic() {
        GetComponent<AudioSource>().Play();
    }

    void StopMusic() {
        GetComponent<AudioSource>().Stop();
    }
}
