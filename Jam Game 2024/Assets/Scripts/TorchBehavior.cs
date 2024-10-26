using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBehavior : MonoBehaviour, IInteractable
{
    private GameObject player;
    public bool activated;
    [SerializeField] private ParticleSystem flame;
    public AudioSource fire;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        flame.Stop();
        activated = false;
        fire.enabled = false;
    }

    void Update()
    {
        if (activated && Vector3.Distance(player.transform.position, transform.position) <= 3)
        {
            fire.enabled = true;
            if (player.GetComponent<playermovement>().currentTemp < player.GetComponent<playermovement>().maxTemp) {
                player.GetComponent<playermovement>().currentTemp += 2;
                if (player.GetComponent<playermovement>().currentTemp > player.GetComponent<playermovement>().maxTemp)
                    player.GetComponent<playermovement>().currentTemp = player.GetComponent<playermovement>().maxTemp;
            }


        }
        
    }
    public void Interact()
    {
        flame.Play();
        activated = true;
        Debug.Log("Activated" + activated);
    }
}
