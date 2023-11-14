using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TeleportCameraSwitch : MonoBehaviour
{
    public List<CinemachineVirtualCamera> camList;

    public CinemachineVirtualCamera targetCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            foreach (var camera in camList)
            {
                camera.enabled = false;
            }
            targetCam.enabled = true;
        }
    }
}
