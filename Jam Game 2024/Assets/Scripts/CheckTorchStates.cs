using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTorchStates : MonoBehaviour
{
    public List<GameObject> taggedObjects;
    private int activeCount;
    public GameManager manager;
    private bool runOnce;
    void Start()
    {
        //taggedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Torch"));
        activeCount = 0;
        runOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (runOnce)
        {
            taggedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Torch"));
            runOnce = false;
        }

        for (int i = taggedObjects.Count - 1; i >= 0; i--)
        {
            if (taggedObjects[i].GetComponent<TorchBehavior>().activated)
            {
                activeCount++;
                taggedObjects.RemoveAt(i);
            }
        }

        if (activeCount == 7)
        {
            manager.CompleteLevel();
        }
    }
}
