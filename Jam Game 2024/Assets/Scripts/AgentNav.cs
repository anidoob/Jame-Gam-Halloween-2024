using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float sightRange, attackRange;
    private float cellSize;

    private bool canSeePlayer, canAtkPlayer;
    private bool hasWalkPoint;

    private NavMeshAgent agent;
    private GameObject Maze, player;
    private MazeGenerator MazeGen;
    private MazeRenderer MazeRenderer;

    [SerializeField]private GameManager GameManager;

    public LayerMask groundLayer, playerLayer;

    private Vector3 destPoint;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Maze = GameObject.Find("Maze");
        player = GameObject.Find("Player");
        MazeGen = Maze.GetComponent<MazeGenerator>();
        MazeRenderer = Maze.GetComponent<MazeRenderer>();
        cellSize = MazeRenderer.CellSize;
        
    }

    // Update is called once per frame
    void Update()
    {
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (canSeePlayer)
        {
            Chase();
        }
        else 
        {
            Patrol();
        }
        
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
        
        float xRange = Random.Range(MazeGen.startX, cellSize * MazeGen.mazeWidth);
        float zRange = Random.Range(MazeGen.startY, cellSize * MazeGen.mazeHeight);

        destPoint = new Vector3(xRange, 0, zRange);
        if(Physics.Raycast(destPoint, Vector3.down, groundLayer))
            hasWalkPoint = true;
    }

    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Caught!!!");
            GameManager.EndGame();
        }
    }
}
