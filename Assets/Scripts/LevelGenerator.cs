using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    private float xDiff = 1.1f;
    private float yDiffSmall = .4f;
    private float yDiffBig = 1.35f;

    private float xPos = -2.5f;
    private float yPos = -4.5f;

    private string smallTag = "smallTile";
    private string bigTile = "bigTile"; 

    private void Start()
    {               
        for(int i = 0; i < 7; i++)
        {
            GenerateTiles();
        }
    }

    public void GenerateTiles()
    {
        int random = Random.Range(0, 5);
        if(random <= 2)
        {
            GenerateSmallTile();
        }
        else
        {
            GenerateBigTile();
        }
    }

    private void GenerateBigTile()
    {
        xPos += xDiff;
        yPos += yDiffBig;

        tilePrefab.tag = bigTile;
        
        Instantiate(tilePrefab, new Vector3(xPos, yPos, 0), tilePrefab.transform.rotation);
       
    }

    private void GenerateSmallTile()
    {
        xPos += xDiff;
        yPos += yDiffSmall;

        tilePrefab.tag = smallTag;

        Instantiate(tilePrefab,new Vector3(xPos, yPos, 0),tilePrefab.transform.rotation);
      

    }
}
