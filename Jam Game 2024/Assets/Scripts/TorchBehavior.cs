using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBehavior : MonoBehaviour, IInteractable
{
    private GameObject player;
    public bool activated;
    [SerializeField] private ParticleSystem flame;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        flame.Stop();
        activated = false;
    }

    void Update()
    {
        if (activated && Vector3.Distance(player.transform.position, transform.position) <= 3)
        {
            if (player.GetComponent<playermovement>().currentTemp < player.GetComponent<playermovement>().maxTemp) {
                player.GetComponent<playermovement>().currentTemp += 2;
                if (player.GetComponent<playermovement>().currentTemp > player.GetComponent<playermovement>().maxTemp)
                    player.GetComponent<playermovement>().currentTemp = player.GetComponent<playermovement>().maxTemp;
            }


        }
    }

    //IEnumerator AddHeat()
    //{
    //    yield return new WaitForSeconds(3);
        
    //}
    public void Interact()
    {
        flame.Play();
        activated = true;
        Debug.Log("Activated" + activated);
    }
}
