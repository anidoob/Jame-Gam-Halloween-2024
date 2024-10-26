using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class LightFollowScript : MonoBehaviour
{
    public Camera mainCamera;
    void Update()
    {
        //transform.position = mainCamera.transform.position - new Vector3(0f, 0.5f, 0f);
        transform.rotation = mainCamera.transform.rotation;
        
    }
}
