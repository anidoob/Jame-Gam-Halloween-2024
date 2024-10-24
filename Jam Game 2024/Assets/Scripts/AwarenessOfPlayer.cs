using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwarenessOfPlayer : MonoBehaviour
{
    public bool awareofPlayer {get; private set;}
    public Vector2 playerDirection { get; private set; }

    [SerializeField]
    private float awarenessDistance;

    private Transform player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distanceFromPlayer = player.position - transform.position;
        playerDirection = distanceFromPlayer.normalized;

        if (distanceFromPlayer.magnitude <= awarenessDistance)
        {
            awareofPlayer = true;
        }
        else
        {
            awareofPlayer = false;
        }
    }
}
