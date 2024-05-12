using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class LevelSelector : MonoBehaviour
{

    public void LoadLevelPicker()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
