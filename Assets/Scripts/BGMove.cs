using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    private GameObject player;
    Vector3 vel;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.GetComponent<Player>().isDead)
            return;

        transform.position= Vector3.SmoothDamp(
            transform.position,new Vector3(Camera.main.transform.position.x +3f,player.transform.position.y,transform.position.z),ref vel, 1f);
    }


}
