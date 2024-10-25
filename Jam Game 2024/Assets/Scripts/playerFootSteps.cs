using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFootSteps : MonoBehaviour
{
    public AudioSource footsteps;
    public AudioSource sprint;
    private playermovement playermovement;

    private void Start()
    {
        playermovement = GetComponent<playermovement>();
        sprint.enabled = false;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            footsteps.enabled = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footsteps.enabled = false;
                sprint.enabled = true;
            }
            else
            { 
                sprint.enabled = false;
            }
        }
        else
        {
            footsteps.enabled = false;
            sprint.enabled = false;
        }

    }


}
