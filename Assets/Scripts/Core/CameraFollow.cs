using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera class that is used to follow the player
public class CameraFollow : MonoBehaviour
{

    public Transform player; // Vector position of the player that the Camera will follow 
    public Vector3 offset; // Sets the offset of the camera
    private void FixedUpdate()
    {
        transform.position = player.position+offset; 
    }
}
