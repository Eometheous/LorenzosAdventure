using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1, camera2;

    public bool updown = false;

    public void OnTriggerStay2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            if (updown)
            {
                if (other.GetComponent<Rigidbody2D>().velocity.y > .5)
                {
                    camera1.gameObject.SetActive(false);
                    camera2.gameObject.SetActive(true);
                }
                else if (other.GetComponent<Rigidbody2D>().velocity.y < -.5)
                {
                    camera2.gameObject.SetActive(false);
                    camera1.gameObject.SetActive(true);
                }
            }
            else
            {
                if (other.GetComponent<Rigidbody2D>().velocity.x > 0)
                {
                    camera1.gameObject.SetActive(false);
                    camera2.gameObject.SetActive(true);
                }
                else if (other.GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    camera2.gameObject.SetActive(false);
                    camera1.gameObject.SetActive(true);
                }
            }
        }
    }
}
