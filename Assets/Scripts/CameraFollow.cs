using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 velocity;
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player.GetComponent<Player>().isDead)
            return;
        
        Vector3 target = new Vector3(player.transform.position.x + 3f, player.transform.position.y,transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, .5f);
    }
}
