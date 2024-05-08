using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera camera;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            camera.transform.position = transform.position + new Vector3(0, 0, -10);
        }
    }
}
