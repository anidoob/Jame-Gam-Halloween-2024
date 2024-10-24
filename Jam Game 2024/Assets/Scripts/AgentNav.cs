using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 destPoint;
    private AwarenessOfPlayer AwarenessOfPlayer;
    private NavMeshAgent agent;
    private bool hasWalkPoint;
    private GameObject Maze;
    private MazeGenerator MazeGen;
    private MazeRenderer MazeRenderer;
    public LayerMask groundLayer;

    private float xRange;
    private float zRange;
    private float cellSize;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Maze = GameObject.Find("Maze");
        MazeGen = Maze.GetComponent<MazeGenerator>();
        MazeRenderer = Maze.GetComponent<MazeRenderer>();
        cellSize = MazeRenderer.CellSize;
    }

    // Update is called once per frame
    void Update()
    {
        //if (AwarenessOfPlayer.awareofPlayer)
            Patrol();
        
    }

    void Patrol()
    {
        if (!hasWalkPoint)
        {
            searchforDestination();
        }
        if (hasWalkPoint)
        {
            agent.SetDestination(destPoint);
        }
        if(Vector3.Distance(transform.position, destPoint) <= 10) hasWalkPoint = false;

    }

    void searchforDestination()
    {
        
        xRange = Random.Range(MazeGen.startX, cellSize * MazeGen.mazeWidth);
        zRange = Random.Range(MazeGen.startY, cellSize * MazeGen.mazeHeight);

        destPoint = new Vector3(xRange, 0, zRange);
        if(Physics.Raycast(destPoint, Vector3.down, groundLayer))
            hasWalkPoint = true;
    }
}
