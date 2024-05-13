using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MusicKaput : MonoBehaviour
{

    public GameObject musicObject; 
    public GameObject button;



    public void Start(){
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(DestroyMusic);
    }
    public void DestroyMusic()
    {
        if(musicObject != null){
        Destroy(musicObject);
        }
    
    }


}