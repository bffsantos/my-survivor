using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10.0f);
        }
    }
}
