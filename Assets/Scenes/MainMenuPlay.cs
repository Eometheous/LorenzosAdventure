using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuPlay : MonoBehaviour

{
    public void LoadFirstLevel()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


}
