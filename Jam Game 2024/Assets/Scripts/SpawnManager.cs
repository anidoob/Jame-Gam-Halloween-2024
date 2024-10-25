using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject torch;
    public GameObject Maze;
    Vector3 spawnPosition;

    [SerializeField] private int numberofSpawns = 7;
    
    void Start()
    {
        int xbound = Maze.GetComponent<MazeGenerator>().mazeWidth;
        int zbound = Maze.GetComponent<MazeGenerator>().mazeHeight;

         
        
        for(int i = 0; i < numberofSpawns; i++) 
        {
            spawnPosition = new Vector3(getMultipleofFive(xbound), 0.5f, getMultipleofFive(zbound));
            Instantiate(torch, spawnPosition, Quaternion.identity);
        }

    }

    
    void Update()
    {
        
    }

    int getMultipleofFive(int bound)
    {
        return Random.Range(1,bound) * 5;
    }
}
