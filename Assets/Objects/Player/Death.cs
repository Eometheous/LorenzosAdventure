using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip DEATH;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            audioSource.PlayOneShot(DEATH);
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
            if (!audioSource.isPlaying) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
