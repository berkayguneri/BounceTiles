using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    float yPosition;
    LevelGenerator generator;
    private void Start()
    {
        yPosition = transform.position.y;
        generator = GameObject.Find("TilesGenerator").GetComponent<LevelGenerator>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < yPosition - 10f)
        {
            generator.GenerateTiles();
            Destroy(this.gameObject);
        }
    }
}
